using AppCommonMethods;
using AuthDAL.Models.Dto.ProfileDto;
using AuthDAL.Models.Dto.ProfileTypeDto;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.DentalDto;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using System;


namespace HMIS.Patient.Service
{
    public class DentalService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly UnitOfWork<DentalSterilizationRecord> _uowDentalSterilizationRecord;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor

        public DentalService(
            TokenService tokenService,
            UnitOfWork<DentalSterilizationRecord> uowDentalSterilizationRecord,
            IMapper mapper
            )
        {
            _tokenService = tokenService;
            _uowDentalSterilizationRecord = uowDentalSterilizationRecord;
            _mapper = mapper;

        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditDentalSterilizationRecordDto> CreateOrEdit(CreateOrEditDentalSterilizationRecordDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.DentalSterilizationRecordId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditDentalSterilizationRecordDto> Create(CreateOrEditDentalSterilizationRecordDto input)
        {
            var obj = _mapper.Map<DentalSterilizationRecord>(input);
            obj.HealthFacilityId = TokenService.GetUserHfId();
            FillEntity(obj);
            DentalSterilizationRecord responseObj = await _uowDentalSterilizationRecord.Repository.Insert(obj);
            await _uowDentalSterilizationRecord.CommitAsync();
            return _mapper.Map<CreateOrEditDentalSterilizationRecordDto>(responseObj);
        }

        private async Task<CreateOrEditDentalSterilizationRecordDto> Update(CreateOrEditDentalSterilizationRecordDto input)
        {
            var dbObj = await _uowDentalSterilizationRecord.Repository.GetById(input.DentalSterilizationRecordId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowDentalSterilizationRecord.Repository.Update(obj!);
            await _uowDentalSterilizationRecord.CommitAsync();

            return _mapper.Map<CreateOrEditDentalSterilizationRecordDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowDentalSterilizationRecord.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowDentalSterilizationRecord.Repository.Update(dbObj!);
            await _uowDentalSterilizationRecord.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations
        public async Task<ViewPagerDto<ViewDentalSterilizationRecordDto>> GetAllWithPagination(FilterDentalSterilizationRecordDto filter)
        {
            var list = _uowDentalSterilizationRecord.Repository.GetALL(x => x.HealthFacilityId == TokenService.GetUserHfId() && x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewDentalSterilizationRecordDto> IQueryableList = list.Select(x =>
               new ViewDentalSterilizationRecordDto
               {
                   DentalSterilizationRecordId  = x.DentalSterilizationRecordId,
                   EquipmentProfileId = x.EquipmentProfileId,
                   EquipmentName = x.EquipmentProfile!.Name,
                   NoOfPouches = x.NoOfPouches,
                   CloseToExpiryDentalMaterial = x.CloseToExpiryDentalMaterial,
                   CreatedBy = x.CreatedByNavigation!.FullName,
                   IsActive = x.IsActive ?? false,
                   Remarks = x.Remarks
               });

            var pagedList = await PagedListDto<ViewDentalSterilizationRecordDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewDentalSterilizationRecordDto>
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

        private void FillEntity(DentalSterilizationRecord obj)
        {
            if (obj.DentalSterilizationRecordId == Guid.Empty)
            {
                obj.DentalSterilizationRecordId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        private void FillEntityDelete(DentalSterilizationRecord obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
