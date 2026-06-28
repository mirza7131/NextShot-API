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
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Service
{
    public class BudgetService
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

        public BudgetService(
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

        #region CUD Operations
        public async Task<CreateOrEditBudgetDto> CreateOrEdit(CreateOrEditBudgetDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.BudgetId))
                return await Create(input);
            else
                return await Update(input);
        }

        public async Task<bool> BulkCreateOrEdit(CreateOrEditBudgetDto input)
        {
            foreach (var item in input.HealthFacilityIds)
            {
                await CreateOrEdit(new CreateOrEditBudgetDto
                {
                    HealthFacilityId = item,
                    AllocatedAmount = input.AllocatedAmount,
                    IsBudgetAllocated = true
                });

            }
            return true;
        }

        private async Task<CreateOrEditBudgetDto> Create(CreateOrEditBudgetDto input)
        {
            var obj = _mapper.Map<Budget>(input);
            FillEntity(obj);
            obj.IsBudgetAllocated = true;
            Budget responseObj = await _uowBudget.Repository.Insert(obj);
            await _uowBudget.CommitAsync();

            return _mapper.Map<CreateOrEditBudgetDto>(responseObj);
        }

        private async Task<CreateOrEditBudgetDto> Update(CreateOrEditBudgetDto input)
        {
            var dbObj = await _uowBudget.Repository.GetById(input.BudgetId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //var obj = _mapper.Map(input, dbObj);
            dbObj.AllocatedAmount = input.AllocatedAmount;
            FillEntity(dbObj!);
            dbObj.IsBudgetAllocated = true;
            _uowBudget.Repository.Update(dbObj!);
            await _uowBudget.CommitAsync();

            return _mapper.Map<CreateOrEditBudgetDto>(dbObj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowBudget.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowBudget.Repository.Update(dbObj!);
            await _uowBudget.CommitAsync();

            return true;
        }


        #region DG Office

        public async Task<ReleasebudgetDto> UpdateReleaseBudget(ReleasebudgetDto input)
        {
            var obj = await _uowBudget.Repository.GetALL(x => x.DiaryNo == input.DiaryNo).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(obj))
                throw new UserFriendlyException(CommonMessageConstant.DairayNoAlreadyExits);


            obj = await _uowBudget.Repository.GetALL(x => x.ChequeNo == input.ChequeNo).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(obj))
                throw new UserFriendlyException(CommonMessageConstant.ChequeNoAlreadyExits);


            var dbObj = await _uowBudget.Repository.GetById(input.BudgetId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            dbObj = _mapper.Map(input, dbObj);
            dbObj.IsChequeIssue = true;
            FillEntity(dbObj!);

            _uowBudget.Repository.Update(dbObj!);
            await _uowBudget.CommitAsync();

            return _mapper.Map<ReleasebudgetDto>(dbObj);
        }


        public async Task<bool> RemoveImage(ChequeImageDto chequeImageDto)
        {

            if (!AppCommonMethod.IsNullObject(chequeImageDto))
            {
                if (!AppCommonMethod.IsNullOrEmptyGuid(chequeImageDto.BudgetId))
                {
                    var dbObj = await _uowBudget.Repository.GetById(chequeImageDto.BudgetId!);

                    if (AppCommonMethod.IsNullObject(dbObj))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


                    dbObj.ChequeImage = "";
                    FillEntity(dbObj!);

                    _uowBudget.Repository.Update(dbObj!);
                    await _uowBudget.CommitAsync();
                }
                else
                    return false;

            }
            else
                return false;

            return true;
        }


        public async Task<string> SaveChequeImage(ChequeImageDto chequeImageDto)
        {
            var imageUrl = await _fileUploader.UploadFileToCDN(CommonStringConstant.HealthCouncil, chequeImageDto?.ChequeImageBase64, _tokenService.GetAccessToken());
            if (!string.IsNullOrEmpty(imageUrl))
            {

                var dbObj = await _uowBudget.Repository.GetById(chequeImageDto.BudgetId!);

                if (AppCommonMethod.IsNullObject(dbObj))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


                dbObj.ChequeImage = imageUrl;
                FillEntity(dbObj!);

                _uowBudget.Repository.Update(dbObj!);
                await _uowBudget.CommitAsync();


                return imageUrl;
            }
            return "";
        }


        #endregion



        #region HealthCouncil


        public async Task SaveBankStatement(BankStatementDto bankStatement)
        {
            var _uowBankStatement = new UnitOfWork<BankStatement>(_uowBudget.GetDbContext());
            BankStatement statement = new BankStatement();
            var imageUrl = await _fileUploader.UploadFileToCDN(CommonStringConstant.HealthCouncil, bankStatement?.File, _tokenService.GetAccessToken());
            if (!string.IsNullOrEmpty(imageUrl))
            {
                bankStatement.File = imageUrl;
            }
            statement = _mapper.Map<BankStatement>(bankStatement);

            FillEntityBankStatement(statement);

            await _uowBankStatement.Repository.Insert(statement);
            await _uowBankStatement.Save();

        }


        public async Task CreateContigentStaff(ContigmentStaffDto contigmentStaffDto)
        {
            var _uowContignetStaff = new UnitOfWork<ContignetStaff>(_uowBudget.GetDbContext());

            var dbObj = await _uowContignetStaff.Repository.GetALL(x => x.Cnic == contigmentStaffDto.Cnic).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.UserAlreadyExistsWithCnic);


            dbObj = await _uowContignetStaff.Repository.GetALL(x => x.PhoneNo == contigmentStaffDto.PhoneNo).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.UserAlreadyExistsWithPhoneNo);


            dbObj = await _uowContignetStaff.Repository.GetALL(x => x.BankAccountNo == contigmentStaffDto.BankAccountNo).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.UserAlreadyExistsWithAccountNo);

            ContignetStaff contignetStaff = new ContignetStaff();
            contignetStaff = _mapper.Map<ContignetStaff>(contigmentStaffDto);

            FillEntityContignetStaff(contignetStaff);

            await _uowContignetStaff.Repository.Insert(contignetStaff);
            await _uowContignetStaff.Save();
        }


        public async Task EditContigentStaff(ContigmentStaffDto contigmentStaffDto)
        {
            if (AppCommonMethod.IsNullObject(contigmentStaffDto))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            if (AppCommonMethod.IsNullOrEmptyGuid(contigmentStaffDto.ContignetStaffId))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            var _uowContignetStaff = new UnitOfWork<ContignetStaff>(_uowBudget.GetDbContext());
            var dbObj = await _uowContignetStaff.Repository.GetALL(x => x.ContignetStaffId == contigmentStaffDto.ContignetStaffId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            dbObj = _mapper.Map(contigmentStaffDto, dbObj);

            FillEntityContignetStaff(dbObj);

            _uowContignetStaff.Repository.Update(dbObj);
            await _uowContignetStaff.CommitAsync();
        }



        public async Task CreateOrEditContigentStaff(ContigmentStaffDto contigmentStaffDto)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(contigmentStaffDto.ContignetStaffId))
            {
                await CreateContigentStaff(contigmentStaffDto);
            }
            else
            {
                await EditContigentStaff(contigmentStaffDto);
            }

        }



        public async Task UpdateCheuqeStatus(ChequeStatusDto chequeStatusDto)
        {
            var dbObj = await _uowBudget.Repository.GetById(chequeStatusDto.BudgetId);
            if (!AppCommonMethod.IsNullObject(dbObj))
            {
                dbObj.ChequeReceivedStatusProfileId = chequeStatusDto.ChequeStatusProfileId;

                if (chequeStatusDto.ProfileName == CommonStringConstant.Received)
                {
                    var _uowHealthFacilityBankDEatil = new UnitOfWork<HealthFacilityBankDetail>(_uowBudget.GetDbContext());
                    var bankDetail = await _uowHealthFacilityBankDEatil.Repository.GetALL(x => x.HealthFacilityId == dbObj.HealthFacilityId).FirstOrDefaultAsync();
                    if (!AppCommonMethod.IsNullObject(bankDetail))
                    {
                        bankDetail.CurrentBalance += chequeStatusDto.AllocatedAmount;
                        _uowHealthFacilityBankDEatil.Repository.Update(bankDetail);
                        await _uowHealthFacilityBankDEatil.CommitAsync();
                    }
                }

                _uowBudget.Repository.Update(dbObj);
                await _uowBudget.CommitAsync();
            }
        }
        #endregion


        #endregion

        #region Read Operations

        public async Task<List<ViewAllocateBudgetStatsDto>> GetFacilityTypeCountAndBalance(UserLevelFilterDto filter)
        {

            var listHf = await GetAllHealthFacilities(filter);
            var healthFaciliytIds = listHf.Where(x => hfTypesAllowed.Contains(x.HealthFacilityTypeCode)).Select(x => x.HealthFacilityId).ToList();

            var allListHfType = await _locationService.GetAllHealthFacilityTypes(_authBaseUrl);

            var listHfType = allListHfType.data.Where(x => hfTypesAllowed.Contains(x.Code)).ToList();

            var hfBanks = await _uowBudget.GetDbContext().HealthFacilityBankDetails
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn >= filter.StartDate)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn <= filter.EndDate)
                .ToListAsync();
            var budgets = await _uowBudget.GetDbContext().Budgets
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn >= filter.StartDate)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn <= filter.EndDate)
                .ToListAsync();
            var expenses = await _uowBudget.GetDbContext().Expenses
                 .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn >= filter.StartDate)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn <= filter.EndDate)
                .ToListAsync();

            List<ViewAllocateBudgetStatsDto> listViewAllocateBudgetStatsDto = new List<ViewAllocateBudgetStatsDto>();

            foreach (var item in listHfType)
            {
                var temphfIds = listHf.Where(t => t.HealthFacilityTypeId == item.HealthFacilityTypeId)
                                        .Select(x => x.HealthFacilityId).ToList();

                listViewAllocateBudgetStatsDto.Add(new ViewAllocateBudgetStatsDto
                {
                    HealthFacilityType = item.Name,
                    HealthFacilityTypeId = item.HealthFacilityTypeId,
                    HealthFacilityCount = listHf.Where(x => x.HealthFacilityTypeId == item.HealthFacilityTypeId).Count(),
                    RequestForBudget = budgets.Where(x => temphfIds.Contains((int)x.HealthFacilityId)).Sum(x => x.AllocatedAmount),
                    IssuedBudget = budgets.Where(x => temphfIds.Contains((int)x.HealthFacilityId) && x.IsChequeIssue == true && x.AllocatedAmount != null && x.ReleaseAmount != null).Sum(x => x.AllocatedAmount),
                    Expenses = expenses.Where(x => temphfIds.Contains((int)x.HealthFacilityId)).Sum(x => x.ExpenseAmount),
                    InitialBalance = hfBanks.Where(x => temphfIds.Contains((int)x.HealthFacilityId)).Select(x => x.OpeningBalance).FirstOrDefault() ?? 0,
                    CurrentBankBalance = hfBanks.Where(x => temphfIds.Contains((int)x.HealthFacilityId)).Sum(x => x.CurrentBalance),
                    BankAccountCount = hfBanks.Where(x => temphfIds.Contains((int)x.HealthFacilityId)).Count(),
                });
            }



            return listViewAllocateBudgetStatsDto;


        }

        public async Task<ViewPagerDto<ViewAllocateBudgetDto>> GetAllocateBudget(UserLevelFilterDto filter)
        {
            //filter.PageSize = 1000000;
            var listHf = await _locationService.GetAllHealthFacilityWithPagination(_authBaseUrl, filter.DivisionId, filter.DistrictId, filter.TehsilId, filter.HealthFacilityId, 1000000);
            var healthFaciliytIds = listHf.data.List.Select(x => x.HealthFacilityId).ToList();


            var list = _uowBudget.GetDbContext().ViewAllocateBudgets
                .Where(x => healthFaciliytIds.Contains((int)x.HealthFacilityId))
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewAllocateBudgetDto> IQueryableList = list.Select(x => new ViewAllocateBudgetDto
            {
                BudgetId = x.BudgetId,
                IsChequeIssue = x.IsChequeIssue,
                IsBudgetAllocated = x.IsBudgetAllocated,
                AllocatedAmount = x.AllocatedAmount,
                HealthFacilityId = x.HealthFacilityId,
                IsActive = (bool)x.IsActive,
                CreatedOn = x.CreatedOn,
                ChequeReceivedStatusProfileId = x.ChequeReceivedStatusProfileId

            });


            var pagedList = await PagedListDto<ViewAllocateBudgetDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<ViewAllocateBudgetDto>
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

        public async Task<ViewPagerDto<ViewAllocateBudgetDto>> GetChequeIssue(UserLevelFilterDto filter)
        {
            filter.PageSize = 1000000;
            var listHf = await _locationService.GetAllHealthFacilityWithPagination(_authBaseUrl, filter.DivisionId, filter.DistrictId, filter.TehsilId, filter.HealthFacilityId, filter.PageSize);
            var healthFaciliytIds = listHf.data.List.Select(x => x.HealthFacilityId).ToList();


            var list = _uowBudget.GetDbContext().ViewChequeIssues
                .Where(x => healthFaciliytIds.Contains((int)x.HealthFacilityId))
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewAllocateBudgetDto> IQueryableList = list.Select(x => new ViewAllocateBudgetDto
            {
                BudgetId = x.BudgetId,
                IsChequeIssue = x.IsChequeIssue,
                ReleasedAmount = x.ReleaseAmount,
                ChequeNo = Convert.ToString(x.ChequeNo),
                CourierCompany = x.CourierCompany,
                CourierDispatchDate = x.CourierDispatchDate,
                DiaryNo = x.DiaryNo,
                HealthFacilityId = x.HealthFacilityId,
                CreatedOn = x.CreatedOn,
                ChequeReceivedStatusProfileId = x.ChequeReceivedStatusProfileId

            });


            var pagedList = await PagedListDto<ViewAllocateBudgetDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<ViewAllocateBudgetDto>
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
        public async Task<ViewPagerDto<ViewReleaseBudgetDto>> GetReleaseBudget(UserLevelFilterDto filter)
        {
            var listHf = await _locationService.GetAllHealthFacilityWithPagination(_authBaseUrl, filter.DivisionId, filter.DistrictId, filter.TehsilId, filter.HealthFacilityId, 1000000); //1000000 to get all Health facility locations
            var healthFaciliytIds = listHf.data.List.Select(x => x.HealthFacilityId).ToList();

            var list = _uowBudget.GetDbContext().ViewReleaseBudgets.Where(x => healthFaciliytIds.Contains((int)x.HealthFacilityId)).OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewReleaseBudgetDto> IQueryableList = list.Select(x => new ViewReleaseBudgetDto
            {
                BudgetId = x.BudgetId,
                HealthFacilityId = x.HealthFacilityId,
                //HealthFacilityName = x.HealthFacilityName,
                BankAccountStatus = AppCommonMethod.IsNullBool(x.BankAccountStatus) ? false : x.BankAccountStatus,
                AllocatedAmount = x.AllocatedAmount,
                ReleaseAmount = x.ReleaseAmount,
                //ChequeStatus = x.ChequeStatus,
                IsChequeIssue = (bool)x.IsChequeIssue,
                ChequeImage = x.ChequeImage

            });


            var pagedList = await PagedListDto<ViewReleaseBudgetDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<ViewReleaseBudgetDto>
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
        public async Task<ViewPagerDto<ViewAllocateBudgetDto>> GetHealthFacilitiesDetail(UserLevelFilterDto filter)
        {
            var rawListHf = await _locationService.GetAllHealthFacilities(_authBaseUrl, filter.DivisionId, filter.DistrictId, filter.TehsilId, filter.HealthFacilityId);
            var listHf = rawListHf.data.Where(x => x.HealthFacilityTypeId == filter.HealthFacilityTypeId).ToList();
            var healthFaciliytIds = listHf.Select(x => x.HealthFacilityId).ToList();

            var list = await _uowBudget.GetDbContext().ViewAllocateBudgets.Where(x => healthFaciliytIds.Contains((int)x.HealthFacilityId)
            && (x.IsChequeIssue == null || x.IsChequeIssue == false)
            ).OrderByDescending(x => x.CreatedOn).ToListAsync();

            List<ViewAllocateBudgetDto> listAllocateBudgetDto = new List<ViewAllocateBudgetDto>();

            foreach (var item in listHf)
            {
                ViewAllocateBudgetDto tempViewAllocateBudgetDto = new ViewAllocateBudgetDto();

                tempViewAllocateBudgetDto.HealthFacilityId = item.HealthFacilityId;
                tempViewAllocateBudgetDto.HealthFacilityName = item.Name;

                var tempBudgetData = list.Where(x => x.HealthFacilityId == item.HealthFacilityId).FirstOrDefault();

                if (tempBudgetData != null)
                {
                    var _uowExpenses = new UnitOfWork<Expense>(_uowBudget.GetDbContext());

                    tempViewAllocateBudgetDto.BudgetId = tempBudgetData.BudgetId;
                    tempViewAllocateBudgetDto.IsChequeIssue = tempBudgetData.IsChequeIssue;
                    tempViewAllocateBudgetDto.IsBudgetAllocated = tempBudgetData.IsBudgetAllocated;
                    tempViewAllocateBudgetDto.AllocatedAmount = tempBudgetData.AllocatedAmount ?? 0;
                    tempViewAllocateBudgetDto.HealthFacilityId = tempBudgetData.HealthFacilityId;
                    tempViewAllocateBudgetDto.IsActive = (bool)tempBudgetData.IsActive;
                    tempViewAllocateBudgetDto.CreatedOn = tempBudgetData.CreatedOn;
                    tempViewAllocateBudgetDto.CreatedBy = tempBudgetData.CreatedBy;
                    tempViewAllocateBudgetDto.Bank = tempBudgetData.Bank;
                    tempViewAllocateBudgetDto.BankContact = tempBudgetData.BankContact;
                    tempViewAllocateBudgetDto.BranchName = tempBudgetData.BranchName;
                    tempViewAllocateBudgetDto.AccountNo = tempBudgetData.AccountNo;
                    tempViewAllocateBudgetDto.AccountTitle = tempBudgetData.AccountTitle;
                    tempViewAllocateBudgetDto.CurrentBalance = tempBudgetData.CurrentBalance;
                    tempViewAllocateBudgetDto.OpeningBalance = tempBudgetData.OpeningBalance;
                    tempViewAllocateBudgetDto.RequestForBudget = _uowBudget.Repository
                                                                       .GetALL(x => x.HealthFacilityId == item.HealthFacilityId)
                                                                       .Sum(x => x.AllocatedAmount);
                    tempViewAllocateBudgetDto.IssuedBudget = _uowBudget.Repository
                                                                       .GetALL(x => x.HealthFacilityId == item.HealthFacilityId
                                                                        && x.AllocatedAmount != null
                                                                        && x.ReleaseAmount != null)
                                                                       .Sum(x => x.AllocatedAmount);

                    tempViewAllocateBudgetDto.Expenses = _uowExpenses.Repository
                                                                     .GetALL(x => x.HealthFacilityId == item.HealthFacilityId)
                                                                     .Sum(x => x.ExpenseAmount);
                }

                listAllocateBudgetDto.Add(tempViewAllocateBudgetDto);

            }

            var responseObject = new ViewPagerDto<ViewAllocateBudgetDto>
            {
                TotalCount = listAllocateBudgetDto.Count(),
                PageSize = filter.PageSize,
                CurrentPage = filter.PageNumber,
                TotalPages = (int)Math.Ceiling(listAllocateBudgetDto.Count() / (double)1),
                HasNext = false,
                HasPrevious = false,
                List = listAllocateBudgetDto.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList(),
            };

            return responseObject;
        }

        public async Task<ViewPagerDto<BankStatementDto>> GetBankStatements(UserLevelFilterDto filter)
        {
            var _uowBankStatement = new UnitOfWork<BankStatement>(_uowBudget.GetDbContext());

            var bankStatements = _uowBankStatement.Repository
                .GetALL(x => x.CreatedBy == _tokenService.GetUserId())
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn >= filter.StartDate)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn <= filter.EndDate)
                .OrderByDescending(x => x.CreatedOn);


            IQueryable<BankStatementDto> IQueryableList = bankStatements.Select(x => new BankStatementDto
            {
                Month = x.Month,
                Discription = x.Discription,
                File = x.File,
                CreatedOn = x.CreatedOn
            });


            var pagedList = await PagedListDto<BankStatementDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<BankStatementDto>
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
        public async Task<Budget> GetChequeImage(Guid BudgetId)
        {

            var budgetData = await _uowBudget.Repository.GetById(BudgetId);
            if (!AppCommonMethod.IsNullObject(budgetData))
            {
                return budgetData;
            }
            return null;
        }

        public async Task<ViewPagerDto<ContigmentStaffDto>> GetContigentStaffList(UserLevelFilterDto filter)
        {
            var _uowContignetStaff = new UnitOfWork<ContignetStaff>(_uowBudget.GetDbContext());

            var contignetStaff = _uowContignetStaff.Repository
                .GetALL(x => x.CreatedBy == _tokenService.GetUserId())
                .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.Cnic == filter.Cnic)
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.PhoneNo == filter.MobileNo)
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ContigmentStaffDto> IQueryableList = contignetStaff.Select(x => new ContigmentStaffDto
            {
                ContignetStaffId = x.ContignetStaffId,
                Name = x.Name,
                FatherName = x.FatherName,
                PhoneNo = x.PhoneNo,
                Cnic = x.Cnic,
                Salary = x.Salary,
                ContractStartDate = x.ContractStartDate,
                HealthFacilityId = x.HealthFacilityId,
                ContractExpiryDate = x.ContractExpiryDate,
                BankBranchName = x.BankBranchName,
                BankBranchCode = x.BankBranchCode,
                BankAccountTitle = x.BankAccountTitle,
                BankAccountNo = x.BankAccountNo,
                BankName = x.BankName,
                CreatedOn = x.CreatedOn,
                CreatedBy = x.CreatedBy,

            });


            var pagedList = await PagedListDto<ContigmentStaffDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<ContigmentStaffDto>
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
        public async Task<List<ViewHealthFacilityDto>> GetAllHealthFacilities(UserLevelFilterDto filter)
        {
            var listHf = await _locationService.GetAllHealthFacilities(_authBaseUrl, filter.DivisionId, filter.DistrictId, filter.TehsilId, filter.HealthFacilityId);

            var healthFacilities = listHf.data.Select(x => new ViewHealthFacilityDto
            {
                ProvinceId = x.ProvinceId,
                DivisionId = x.DivisionId,
                DistrictId = x.DistrictId,
                TehsilId = x.TehsilId,
                HealthFacilityId = x.HealthFacilityId,
                HealthFacilityCode = x.Code,
                DivisionCode = x.DivisionCode,
                DistrictCode = x.DistrictCode,
                TehsilCode = x.TehsilCode,
                Name = x.Name,
                Code = x.Code,
                HealthFacilityTypeCode = x.HealthFacilityTypeCode,
                HealthFacilityTypeId = x.HealthFacilityTypeId
            }).ToList();

            return healthFacilities;
        }


        public async Task<ViewPagerDto<BudgetDto>> GetIssuedCheques(UserLevelFilterDto filter)
        {
            var issuedChequesList = _uowBudget.Repository.GetALL(x => x.HealthFacilityId == filter.HealthFacilityId && x.IsChequeIssue == true).OrderByDescending(x => x.ChequeDate);


            IQueryable<BudgetDto> IQueryableList = issuedChequesList.Select(x => new BudgetDto
            {
                BudgetId = x.BudgetId,
                ChequeIssueDate = x.ChequeDate,
                ChequeNo = x.ChequeNo,
                AllocatedAmount = x.AllocatedAmount,
                CourierCompany = x.CourierCompany,
                CourierDispatchDate = x.CourierDispatchDate,
                DiaryNo = x.DiaryNo,
                ChequeReceivedDate = x.ChequeReceivedDate,
                ChequeReceivedStatusProfileId = x.ChequeReceivedStatusProfileId,

            });


            var pagedList = await PagedListDto<BudgetDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<BudgetDto>
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
        private void FillEntity(Budget obj)
        {
            if (obj.BudgetId == Guid.Empty)
            {
                obj.BudgetId = Guid.NewGuid();
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



        private void FillEntityBankStatement(BankStatement obj)
        {
            if (obj.BankStatementId == Guid.Empty)
            {
                obj.BankStatementId = Guid.NewGuid();
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


        private void FillEntityContignetStaff(ContignetStaff obj)
        {
            if (obj.ContignetStaffId == Guid.Empty)
            {
                obj.ContignetStaffId = Guid.NewGuid();
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

        private void FillEntityDelete(Budget obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }

        }

        #endregion
    }
}
