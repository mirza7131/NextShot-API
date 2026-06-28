using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using FileHandler;
using HMIS.Aggregator.API;
using HMIS.HealthCouncil.Domain.Models.DbModels;
using HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil;
using HMIS.HealthCouncil.Domain.Models.DTO.FilterDto;
using HMIS.HealthCouncil.Domain.Models.DTO.PaginationDto;
using HMIS.HealthCouncil.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using HMIS.HealthCouncil.Domain.Models.Dto.BankDetails;
using HMIS.HealthCouncil.Domain.Models.DTO.Budget;

namespace HMIS.HealthCouncil.Service
{
    public class BankDetailService
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

        public BankDetailService(
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


        public async Task CreateBankDetail(BankDetailsDto bankDetailsDto)
        {
            var _uowBankDetail = new UnitOfWork<HealthFacilityBankDetail>();

            var data = await _uowBankDetail.Repository.GetALL(x => x.HealthFacilityId == bankDetailsDto.HealthFacilityId).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(data))
                throw new UserFriendlyException(CommonMessageConstant.BankAccountAlreadyCreated);


            data = await _uowBankDetail.Repository.GetALL(x => x.AccountNo == bankDetailsDto.AccountNo).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(data))
                throw new UserFriendlyException(CommonMessageConstant.BankAccountAlreadyCreated);

            HealthFacilityBankDetail bankDetail = new HealthFacilityBankDetail();
            bankDetail = _mapper.Map<HealthFacilityBankDetail>(bankDetailsDto);
            bankDetail.OpeningBalance = bankDetail.CurrentBalance;

            FillEntityBankDetail(bankDetail);

            await _uowBankDetail.Repository.Insert(bankDetail);
            await _uowBankDetail.Save();
        }


        public async Task EditBankDetail(BankDetailsDto bankDetailsDto)
        {
            var _uowBankDetail = new UnitOfWork<HealthFacilityBankDetail>();
            var dbObj = await _uowBankDetail.Repository.GetById(bankDetailsDto.HealthFacilityBankDetailId);
            if (!AppCommonMethod.IsNullObject(dbObj))
            {
                dbObj = _mapper.Map(bankDetailsDto, dbObj);

                FillEntityBankDetail(dbObj);

                _uowBankDetail.Repository.Update(dbObj);
                await _uowBankDetail.CommitAsync();
            }
        }


        public async Task CreateOrEditBankDetails(BankDetailsDto bankDetailsDto)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(bankDetailsDto.HealthFacilityBankDetailId))
            {
                await CreateBankDetail(bankDetailsDto);
            }
            else
            {
                await EditBankDetail(bankDetailsDto);
            }
        }



        #region Read Operations
        public async Task<ViewPagerDto<BankDetailsDto>> GetBankListByHealthFacility(UserLevelFilterDto filter)
        {
            var _uowBankDetail = new UnitOfWork<HealthFacilityBankDetail>();

            var bankDetailsList = _uowBankDetail.Repository.GetALL()
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.CreatedBy == _tokenService.GetUserId())
                .OrderByDescending(x => x.CreatedOn);



            IQueryable<BankDetailsDto> IQueryableList = bankDetailsList.Select(x => new BankDetailsDto
            {
                HealthFacilityBankDetailId = x.HealthFacilityBankDetailId,
                HealthFacilityId = x.HealthFacilityId,
                Bank = x.Bank,
                BranchName = x.BranchName,
                BranchCode = x.BranchCode,
                AccountTitle = x.AccountTitle,
                AccountNo = x.AccountNo,
                BankContact = x.BankContact,
                CurrentBalance = x.CurrentBalance,
            });


            var pagedList = await PagedListDto<BankDetailsDto>.ToPagedListAsync(
                  IQueryableList,
                  filter.PageNumber,
                  filter.PageSize
                  );

            var responseObject = new ViewPagerDto<BankDetailsDto>
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

        private void FillEntityBankDetail(HealthFacilityBankDetail obj)
        {
            if (obj.HealthFacilityBankDetailId == Guid.Empty)
            {
                obj.HealthFacilityBankDetailId = Guid.NewGuid();
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
