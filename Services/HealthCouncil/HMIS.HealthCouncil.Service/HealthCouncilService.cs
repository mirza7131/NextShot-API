using AppCommonMethods;
using AutoMapper;
using CommonDTOs;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using DTOs.UserDTO;
using FileHandler;
using HMIS.Aggregator.API;
using HMIS.HealthCouncil.Domain.Models.DbModels;
using HMIS.HealthCouncil.Domain.Models.Dto.Budget;
using HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil;
using HMIS.HealthCouncil.Domain.Models.DTO.Budget;
using HMIS.HealthCouncil.Domain.Models.DTO.FilterDto;
using HMIS.HealthCouncil.Domain.Models.DTO.HealthFacility;
using HMIS.HealthCouncil.Domain.Models.DTO.PaginationDto;
using HMIS.HealthCouncil.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HMIS.HealthCouncil.Service
{
    public class HealthCouncilService
    {
        #region Class Fields & Propertities
        private readonly TokenService _tokenService;
        private readonly UnitOfWork<Budget> _uowBudget;
        private readonly IMapper _mapper;
        private readonly LocationService _locationService;
        private readonly string _authBaseUrl;
        private string[] hfTypesAllowed; // Health Facilities Types Allow Only
        private readonly UploadFiles _fileUploader;

        #endregion



        #region Constructor

        public HealthCouncilService(
           TokenService tokenService,
           UnitOfWork<Budget> uowBudget,
           LocationService locationService,
           IConfiguration config,
           IMapper mapper,
            UploadFiles fileUploader
       )
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _uowBudget = uowBudget;
            _mapper = mapper;
            _locationService = locationService;
            _authBaseUrl = config.GetSection("AuthBaseUrl").Value ?? string.Empty;
            hfTypesAllowed = config.GetSection("HealthFacilityTypes").Get<string[]>() ?? new string[0];
            _fileUploader = fileUploader;
        }
        #endregion



        #region CUD

        public async Task CreateCommitteeFormulation(CommitteeFormulationDto committeeFormulationDto)
        {
            var _uowCommitteeFormulation = new UnitOfWork<CommitteeFormulation>();


            var dbObj = await _uowCommitteeFormulation.Repository.GetALL(x => x.Cnic == committeeFormulationDto.Cnic).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.UserAlreadyExistsWithCnic);

            CommitteeFormulation committeeFormulation = new CommitteeFormulation();
            committeeFormulation = _mapper.Map<CommitteeFormulation>(committeeFormulationDto);

            FillEntityCommitteeFormulation(committeeFormulation);

            await _uowCommitteeFormulation.Repository.Insert(committeeFormulation);
            await _uowCommitteeFormulation.Save();
        }


        public async Task EditCommitteeFormulation(CommitteeFormulationDto committeeFormulationDto)
        {
            if (AppCommonMethod.IsNullObject(committeeFormulationDto))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            if (AppCommonMethod.IsNullOrEmptyGuid(committeeFormulationDto.CommitteeFormulationId))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            var _uowCommitteeFormulation = new UnitOfWork<CommitteeFormulation>();
            var dbObj = await _uowCommitteeFormulation.Repository.GetALL(x => x.CommitteeFormulationId == committeeFormulationDto.CommitteeFormulationId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            dbObj = _mapper.Map(committeeFormulationDto, dbObj);

            FillEntityCommitteeFormulation(dbObj);

            _uowCommitteeFormulation.Repository.Update(dbObj);
            await _uowCommitteeFormulation.CommitAsync();
        }


        public async Task CreateMeetingCall(MeetingCallDto meetingCallDto)
        {
            var _uowMeetingCall = new UnitOfWork<MeetingCall>();

            var data = await _uowMeetingCall.Repository.GetALL(x => x.NotificationNo == meetingCallDto.NotificationNo).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(data))
                throw new UserFriendlyException(CommonMessageConstant.NotificationNoAlreadyExists);


            meetingCallDto.NotificationDate = meetingCallDto?.NotificationDate.Value.AddHours(5);
            meetingCallDto.MeetingDate = meetingCallDto?.MeetingDate.Value.AddHours(5);

            MeetingCall meetingCall = new MeetingCall();
            meetingCall = _mapper.Map<MeetingCall>(meetingCallDto);

            FillEntityMeetingCall(meetingCall);

            await _uowMeetingCall.Repository.Insert(meetingCall);
            await _uowMeetingCall.Save();

        }

        public async Task EditMeetingCall(MeetingCallDto meetingCallDto)
        {
            if (AppCommonMethod.IsNullObject(meetingCallDto))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            if (AppCommonMethod.IsNullOrEmptyGuid(meetingCallDto.MeetingCallId))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            var _uowMeetingCall = new UnitOfWork<MeetingCall>();
            var dbObj = await _uowMeetingCall.Repository.GetALL(x => x.MeetingCallId == meetingCallDto.MeetingCallId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            dbObj = _mapper.Map(meetingCallDto, dbObj);

            FillEntityMeetingCall(dbObj);

            _uowMeetingCall.Repository.Update(dbObj);
            await _uowMeetingCall.CommitAsync();
        }
        public async Task CreateMeetingDetail(MeetingDetailDto meetingDetailDto)
        {
            using (var trans = _uowBudget.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var _uowMeetingDetail = new UnitOfWork<MeetingDetail>();

                    var _uowMeetingCall = new UnitOfWork<MeetingCall>();

                    if (!AppCommonMethod.IsNullOrEmptyGuid(meetingDetailDto?.MeetingCallId))
                    {
                        var meetingCallData = await _uowMeetingCall.Repository.GetById(meetingDetailDto.MeetingCallId);
                        if (!AppCommonMethod.IsNullObject(meetingCallData))
                        {
                            meetingCallData.IsMeetingDone = true;
                            _uowMeetingCall.Repository.Update(meetingCallData);
                            await _uowMeetingCall.CommitAsync();
                        }
                    }

                    meetingDetailDto.MeetingDate = meetingDetailDto?.MeetingDate.Value.AddHours(5);

                    MeetingDetail meetingDetail = new MeetingDetail();
                    meetingDetail = _mapper.Map<MeetingDetail>(meetingDetailDto);

                    FillEntityMeetingDetail(meetingDetail);

                    await _uowMeetingDetail.Repository.Insert(meetingDetail);
                    await _uowMeetingDetail.Save();



                    foreach (var category in meetingDetailDto.expenditures)
                    {
                        var _uowMeetingDisscussedCategory = new UnitOfWork<MeetingDisscussedCategory>();
                        MeetingDisscussedCategory meetingDisscussedCategory = new MeetingDisscussedCategory();
                        meetingDisscussedCategory.AccountHeadId = category.AccountHeadId;
                        meetingDisscussedCategory.MeetingDetailId = meetingDetail.MeetingDetailId;
                        FillEntityMeetingDisscussedCategory(meetingDisscussedCategory);

                        await _uowMeetingDisscussedCategory.Repository.Insert(meetingDisscussedCategory);
                        await _uowMeetingDisscussedCategory.Save();


                        foreach (var expendeture in category.Expendetures)
                        {
                            var _uowMeetingExpendeture = new UnitOfWork<MeetingExpendeture>();
                            MeetingExpendeture meetingExpendetures = new MeetingExpendeture();
                            meetingExpendetures = _mapper.Map<MeetingExpendeture>(expendeture);
                            meetingExpendetures.MeetingDetailId = meetingDetail.MeetingDetailId;
                            meetingExpendetures.MeetingDisscussedCategoryId = meetingDisscussedCategory.MeetingDisscussedCategoryId;
                            meetingExpendetures.AccountHeadId = meetingDisscussedCategory.AccountHeadId;
                            meetingExpendetures.HealthFacilityId = meetingDetailDto.HealthFacilityId;


                            FillEntityMeetingExpendetures(meetingExpendetures);

                            await _uowMeetingExpendeture.Repository.Insert(meetingExpendetures);
                            await _uowMeetingExpendeture.Save();


                        }
                    }


                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex.InnerException;
                }
            }
        }




        public async Task CreateVendor(VendorDto vendorDto)
        {
            var _uowVendor = new UnitOfWork<Vendor>(_uowBudget.GetDbContext());

            Vendor vendor = new Vendor();
            vendor = _mapper.Map<Vendor>(vendorDto);
            FillEntityVendor(vendor);
            await _uowVendor.Repository.Insert(vendor);
            await _uowVendor.Save();
        }



        public async Task EditVendor(VendorDto vendorDto)
        {
            var _uowVendor = new UnitOfWork<Vendor>(_uowBudget.GetDbContext());

            var vendor = await _uowVendor.Repository.GetALL(x => x.VendorId == vendorDto.VendorId).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(vendor))
            {
                vendor = _mapper.Map(vendorDto, vendor);
                FillEntityVendor(vendor);
                _uowVendor.Repository.Update(vendor);
                await _uowVendor.CommitAsync();
            }
        }






        public async Task CreateExpense(ExpenseDto expenseDto)
        {
            using (var trans = _uowBudget.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var _uowExpense = new UnitOfWork<Expense>(_uowBudget.GetDbContext());
                    var data = await _uowExpense.Repository
                        .GetALL(x => x.MeetingDetailId == expenseDto.MeetingDetailId && x.MeetingDisscussedCategoryId == expenseDto.MeetingDisscussedCategoryId)
                        .FirstOrDefaultAsync();


                    if (!AppCommonMethod.IsNullObject(data))
                    {
                        throw new UserFriendlyException(CommonMessageConstant.CategoryExpensedAgainstMeeting);
                    }

                    var _uowHealthFacilityBankDetail = new UnitOfWork<HealthFacilityBankDetail>(_uowBudget.GetDbContext());
                    var bankDetail = await _uowHealthFacilityBankDetail.Repository.GetALL(x => x.HealthFacilityId == expenseDto.HealthFacilityId).FirstOrDefaultAsync();
                    if (!AppCommonMethod.IsNullObject(bankDetail))
                    {
                        bankDetail.CurrentBalance -= expenseDto.ExpenseAmount;
                        _uowHealthFacilityBankDetail.Repository.Update(bankDetail);
                        await _uowHealthFacilityBankDetail.CommitAsync();
                    }



                    Expense expense = new Expense();
                    expense = _mapper.Map<Expense>(expenseDto);
                    FillEntityExpense(expense);
                    await _uowExpense.Repository.Insert(expense);
                    await _uowExpense.Save();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }

            }

        }


        public async Task CreateOrEditCommitteeFormulation(CommitteeFormulationDto committeeFormulationDto)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(committeeFormulationDto.CommitteeFormulationId))
            {
                await CreateCommitteeFormulation(committeeFormulationDto);
            }
            else
            {
                await EditCommitteeFormulation(committeeFormulationDto);
            }

        }


        public async Task CreateOrEditMeetingCall(MeetingCallDto meetingCallDto)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(meetingCallDto.MeetingCallId))
            {
                await CreateMeetingCall(meetingCallDto);
            }
            else
            {
                await EditMeetingCall(meetingCallDto);

            }

        }


        public async Task CreateOrEditMeetingDetails(MeetingDetailDto meetingDetailDto)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(meetingDetailDto.MeetingDetailId))
            {
                await CreateMeetingDetail(meetingDetailDto);
            }
            else
            {
                //await EditMeetingCall(meetingDetailDto);

            }

        }


        public async Task CreateOrEditVendor(VendorDto vendorDto)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(vendorDto.VendorId))
            {
                await CreateVendor(vendorDto);
            }
            else
            {
                await EditVendor(vendorDto);
            }

        }




        public async Task CreateOrEditExpenses(ExpenseDto expenseDto)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(expenseDto.ExpenseId))
            {
                await CreateExpense(expenseDto);
            }
            else
            {
                //await EditVendor(vendorDto);
            }

        }


        #endregion



        #region Read Operations


        public async Task<ViewPagerDto<CommitteeFormulationDto>> GetCommitteeFormulationList(UserLevelFilterDto filter)
        {
            var _uowCommitteeFormulation = new UnitOfWork<CommitteeFormulation>();

            var committeeFormulation = _uowCommitteeFormulation.Repository
                .GetALL(x => x.CreatedBy == _tokenService.GetUserId())
                .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.Cnic == filter.Cnic)
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<CommitteeFormulationDto> IQueryableList = committeeFormulation.Select(x => new CommitteeFormulationDto
            {
                CommitteeFormulationId = x.CommitteeFormulationId,
                Name = x.Name,
                Designation = x.Designation,
                MeetingRoleProfileId = x.MeetingRoleProfileId,
                Cnic = x.Cnic,
                HealthFacilityId = x.HealthFacilityId,
                CreatedOn = x.CreatedOn,
                CreatedBy = x.CreatedBy,

            });


            var pagedList = await PagedListDto<CommitteeFormulationDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<CommitteeFormulationDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }

        public async Task<ViewPagerDto<MeetingCallDto>> GetMeetingCallList(UserLevelFilterDto filter)
        {
            var _uowMeetingCall = new UnitOfWork<MeetingCall>();

            var meetingCall = _uowMeetingCall.Repository
                .GetALL(x => x.CreatedBy == _tokenService.GetUserId() )
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<MeetingCallDto> IQueryableList = meetingCall.Select(x => new MeetingCallDto
            {
                MeetingCallId = x.MeetingCallId,
                NotificationNo = x.NotificationNo,
                MeetingAgenda = x.MeetingAgenda,
                NotificationDate = x.NotificationDate,
                MeetingDate = x.MeetingDate,
                MeetingMembers = x.MeetingMembers,
                HealthFacilityId = x.HealthfacilityId,
                CreatedOn = x.CreatedOn,
                CreatedBy = x.CreatedBy,
            });


            var pagedList = await PagedListDto<MeetingCallDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<MeetingCallDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }



        public async Task<ViewPagerDto<MeetingDetailDto>> GetMeetingDetailsList(UserLevelFilterDto filter)
        {
            var _uowMeetingDetail = new UnitOfWork<MeetingDetail>();

            var meetingDetail = _uowMeetingDetail.Repository
                .GetALL(x => x.CreatedBy == _tokenService.GetUserId())
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<MeetingDetailDto> IQueryableList = meetingDetail.Select(x => new MeetingDetailDto
            {
                MeetingDetailId = x.MeetingDetailId,
                MeetingNo = x.MeetingNo,
                MeetingAgenda = x.MeetingAgenda,
                MeetingDate = x.MeetingDate,
                MeetingMembers = x.MeetingMembers,
                HealthFacilityId = x.HealthFacilityId,
                CreatedOn = x.CreatedOn,
                CreatedBy = x.CreatedBy,
            });


            var pagedList = await PagedListDto<MeetingDetailDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<MeetingDetailDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }




        public async Task<List<MeetingCallDto>> GetMeetingCalls(int HealthFacilityId)
        {
            var _uowMeetingCall = new UnitOfWork<MeetingCall>();

            var meetingCall = await _uowMeetingCall.Repository
                .GetALL(x => x.CreatedBy == _tokenService.GetUserId() && x.HealthfacilityId == HealthFacilityId && x.IsMeetingDone == false)
                .OrderByDescending(x => x.CreatedOn).Select(x => new MeetingCallDto
                {
                    MeetingCallId = x.MeetingCallId,
                    MeetingAgenda = x.MeetingAgenda,
                    MeetingDate = x.MeetingDate,
                    MeetingMembers = x.MeetingMembers,
                    HealthFacilityId = x.HealthfacilityId,
                    //IsMeetingDone = x.IsMeetingDone,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy,
                }).ToListAsync();
            return meetingCall;
        }



        public async Task<decimal> GetHealthFacilityAccountBalance(int healthFacilityId)
        {
            var _uowHealthFacilityBankDetail = new UnitOfWork<HealthFacilityBankDetail>();

            var healthFacilityBankDetail = await _uowHealthFacilityBankDetail.Repository.GetALL(x => x.HealthFacilityId == healthFacilityId).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(healthFacilityBankDetail))
            {
                return (decimal)healthFacilityBankDetail.CurrentBalance;
            }
            return 0;
        }

        public async Task<MeetingDetail> GetMeetingDetailsById(Guid MeetingDetailId)
        {
            var _uowMeetingDetail = new UnitOfWork<MeetingDetail>();

            var meetingDetail = await _uowMeetingDetail.Repository.GetALL(x => x.MeetingDetailId == MeetingDetailId).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(meetingDetail))
            {
                return meetingDetail;
            }
            return null;
        }




        public async Task<ViewPagerDto<VendorDto>> GetVendors(UserLevelFilterDto filter)
        {
            var _uowVendor = new UnitOfWork<Vendor>();

            var vendors = _uowVendor.Repository
                .GetALL(x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.ContactNo == filter.MobileNo)
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<VendorDto> IQueryableList = vendors.Select(x => new VendorDto
            {
                VendorId = x.VendorId,
                FirmName = x.FirmName,
                Email = x.Email,
                ContactNo = x.ContactNo,
                Ntn = x.Ntn,
                SalesTaxNo = x.SalesTaxNo,
                Address1 = x.Address1,
                Address2 = x.Address2,
                BankAccountTitle = x.BankAccountTitle,
                BankAccountNo = x.BankAccountNo,
                BankName = x.BankName,
                BankBranchName = x.BankBranchName,
                BankBranchCode = x.BankBranchCode,
                HealthFacilityId = x.HealthFacilityId,
                CreatedOn = x.CreatedOn,
                CreatedBy = x.CreatedBy,
            });


            var pagedList = await PagedListDto<VendorDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<VendorDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }


        //public async Task<ViewPagerDto<MeetingDetailDto>> GetMeetingDetails(Guid MeetingId)
        //{
        //    var _uowMeetingDetail = new UnitOfWork<MeetingDetail>();

        //    var meetingDetail = _uowMeetingDetail.Repository.GetALL(x => x.MeetingDetailId == MeetingId);

        //    if (!AppCommonMethod.IsNullObject(meetingDetail))
        //    {
        //        MeetingDetailDto meetingDetailDto = new MeetingDetailDto();
        //        meetingDetailDto = _mapper.Map<MeetingDetailDto>(meetingDetail);
        //        var _uowMeetingDisscussedCategory = new UnitOfWork<MeetingDisscussedCategory>();
        //        var meetingDisscussedCategories  = await _uowMeetingDisscussedCategory.Repository.GetALL(x=>x.MeetingDetailId== MeetingId).Include(x=>x.AccountHead).ToListAsync();
        //        if(!AppCommonMethod.IsNullOrEmptyList(meetingDisscussedCategories))
        //        {
        //            foreach(var meetingDisscussedCategory in meetingDisscussedCategories)
        //            {
        //                Expenditures expenditures = new Expenditures();

        //                expenditures = _mapper.Map<Expenditures>(meetingDisscussedCategory);

        //                //expenditures.AccountHeadId = meetingDisscussedCategory.AccountHeadId;
        //                //expenditures.MeetingDetailId = meetingDisscussedCategory.MeetingDetailId;
        //                //expenditures.Name = meetingDisscussedCategory?.AccountHead?.Name;
        //            }
        //        }

        //        //MeetingDetailDto meetingDetailDto = new MeetingDetailDto();
        //        //meetingDetailDto = _mapper.Map<MeetingDetailDto>(meetingDetail);
        //        //var _uowMeetingExpendeture = new UnitOfWork<MeetingExpendeture>();
        //        //var meetingDisscussedCategory = await _uowMeetingExpendeture.Repository.GetALL(x => x.MeetingDetailId == MeetingId).Include(x => x.AccountHeadId).Select(x => new MeetingExpendetureDto
        //        //{
        //        //    ItemName = x.ItemName,
        //        //    Quantity = x.Quantity,
        //        //    PricePerUnit = x.PricePerUnit,
        //        //    EstimatedCost = x.EstimatedCost,
        //        //    Name = x.AccountHead.Name
        //        //}).ToListAsync();

        //        //if (AppCommonMethod.IsNullOrEmptyList(meetingDisscussedCategory))
        //        //{
        //        //    meetingDetailDto.expenditures = meetingDisscussedCategory;
        //        //}

        //    }

        //    return null;
        //}




        public async Task<List<AccountHeadDto>> GetAccountHeadsList()
        {
            var _uowAccountHead = new UnitOfWork<AccountHead>();

            var accountHeadList = await _uowAccountHead.Repository
                .GetALL()
                .OrderByDescending(x => x.CreatedOn).Select(x => new AccountHeadDto
                {
                    AccountHeadId = x.AccountHeadId,
                    Name = x.Name,
                }).ToListAsync();

            return accountHeadList;
        }



        public async Task<List<MeetingDetailDto>> GetAccountMeetingsList()
        {
            var _uowMeetingDetail = new UnitOfWork<MeetingDetail>();

            var MeetingDetailList = await _uowMeetingDetail.Repository
                .GetALL(x => x.CreatedBy == _tokenService.GetUserId())
                .OrderByDescending(x => x.CreatedOn).Select(x => new MeetingDetailDto
                {
                    MeetingDetailId = x.MeetingDetailId,
                    MeetingNo = x.MeetingNo,
                    MeetingDate = x.MeetingDate,
                }).ToListAsync();

            return MeetingDetailList;
        }


        public async Task<List<MeetingDisscussedCategoriesDto>> GetMeetingDisscussedCatgories(Guid MeetingId)
        {
            var _uowMeetingDisscussedCategory = new UnitOfWork<MeetingDisscussedCategory>();

            var meetingDisscussedCategories = await _uowMeetingDisscussedCategory.Repository
                .GetALL(x => x.CreatedBy == _tokenService.GetUserId() && x.MeetingDetailId == MeetingId)
                .OrderByDescending(x => x.CreatedOn).Select(x => new MeetingDisscussedCategoriesDto
                {
                    MeetingDisscussedCategoryId = x.MeetingDisscussedCategoryId,
                    MeetingDetailId = x.MeetingDetailId,
                    AccountHeadId = x.AccountHeadId,
                    AccountHeadName = x.AccountHead.Name
                }).ToListAsync();

            return meetingDisscussedCategories;
        }


        public async Task<List<MeetingExpendetureDto>> GetMeetingExpendetures(Guid MeetingDisscussedCategoryId)
        {
            var _uowMeetingExpendeture = new UnitOfWork<MeetingExpendeture>();

            var meetingExpendetures = await _uowMeetingExpendeture.Repository
                .GetALL(x => x.MeetingDisscussedCategoryId == MeetingDisscussedCategoryId)
                .OrderByDescending(x => x.CreatedOn).Select(x => new MeetingExpendetureDto
                {
                    MeetingDisscussedCategoryId = x.MeetingDisscussedCategoryId,
                    MeetingDetailId = x.MeetingDetailId,
                    Name = x.AccountHead.Name,
                    ItemName = x.ItemName,
                    Quantity = x.Quantity,
                    PricePerUnit = x.PricePerUnit,
                    EstimatedCost = x.EstimatedCost,
                }).ToListAsync();

            return meetingExpendetures;
        }



        public async Task<List<VendorDto>> GetAllVendorsHealthFacilityWise(UserLevelFilterDto filter)
        {
            var _uowVendor = new UnitOfWork<Vendor>();

            var vendors = await _uowVendor.Repository
                .GetALL(x => x.HealthFacilityId == filter.HealthFacilityId)
                .OrderByDescending(x => x.CreatedOn).Select(x => new VendorDto
                {
                    VendorId = x.VendorId,
                    FirmName = x.FirmName,
                }).ToListAsync();

            return vendors;
        }


        public async Task<ViewPagerDto<ExpenseListDto>> GetMeetingExpenses(UserLevelFilterDto filter)
        {
            var _uowExpense = new UnitOfWork<Expense>();

            var expenseList = _uowExpense.Repository
                .GetALL(x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.MeetingDetail.MeetingDate >= filter.StartDate)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.MeetingDetail.MeetingDate <= filter.EndDate)
                .OrderByDescending(x => x.CreatedOn).Select(x => new ExpenseListDto
                {
                    VendorId = x.VendorId,
                    FirmName = string.IsNullOrEmpty(x.Vendor.FirmName) ? "" : x.Vendor.FirmName,
                    MeetingNo = string.IsNullOrEmpty(x.MeetingDetail.MeetingNo) ? "" : x.MeetingDetail.MeetingNo,
                    MeetingDate = AppCommonMethod.IsNullorEmptyDate(x.MeetingDetail.MeetingDate) ? null : x.MeetingDetail.MeetingDate,
                    CategoryName = string.IsNullOrEmpty(x.MeetingDisscussedCategory.AccountHead.Name) ? "" : x.MeetingDisscussedCategory.AccountHead.Name,
                    ExpenseAmount = x.ExpenseAmount,
                    ChequeNo = x.ChequeNo,
                    ChequeDate = x.ChequeDate,

                });


            var pagedList = await PagedListDto<ExpenseListDto>.ToPagedListAsync(
                 expenseList,
                 filter.PageNumber,
                 filter.PageSize
                 );

            var responseObject = new ViewPagerDto<ExpenseListDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };


            return responseObject;
        }

        #endregion



        #region Helper Methods
        private void FillEntityCommitteeFormulation(CommitteeFormulation obj)
        {
            if (obj.CommitteeFormulationId == Guid.Empty)
            {
                obj.CommitteeFormulationId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.IsActive = true;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityMeetingCall(MeetingCall obj)
        {
            if (obj.MeetingCallId == Guid.Empty)
            {
                obj.MeetingCallId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.IsActive = true;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityMeetingDetail(MeetingDetail obj)
        {
            if (obj.MeetingDetailId == Guid.Empty)
            {
                obj.MeetingDetailId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.IsActive = true;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityMeetingDisscussedCategory(MeetingDisscussedCategory obj)
        {
            if (obj.MeetingDisscussedCategoryId == Guid.Empty)
            {
                obj.MeetingDisscussedCategoryId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.IsActive = true;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityMeetingExpendetures(MeetingExpendeture obj)
        {
            if (obj.MeetingExpendetureId == Guid.Empty)
            {
                obj.MeetingExpendetureId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.IsActive = true;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }



        private void FillEntityVendor(Vendor obj)
        {
            if (obj.VendorId == Guid.Empty)
            {
                obj.VendorId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.IsActive = true;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }




        private void FillEntityExpense(Expense obj)
        {
            if (obj.ExpenseId == Guid.Empty)
            {
                obj.ExpenseId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.IsActive = true;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        #endregion


    }
}
