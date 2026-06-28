using AppCommonMethods;
using AuthDAL.Models.Dto.ProfileDto;
using AuthDAL.Models.Dto.ProfileTypeDto;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.CdcDto;
using HMIS.Patient.Domain.Models.DTO.DentalDto;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using DbModel = HMIS.Patient.Domain.Models.DbModels;

namespace HMIS.Patient.Service
{
    public class CdcService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly UnitOfWork<DbModel.PatientOpenVisit> _uowPatientOpenVisit;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor

        public CdcService(
            TokenService tokenService,
            UnitOfWork<DbModel.PatientOpenVisit> uowPatientOpenVisit,
            IMapper mapper
        )
        {
            _tokenService = tokenService;
            _uowPatientOpenVisit = uowPatientOpenVisit;
            _mapper = mapper;

        }

        #endregion

        #region CUD Operations

        #endregion

        #region Read Operations
        public async Task<ViewPagerDto<ViewCdcDto>> GetAllWithPagination(FilterCdcDto filter)
        {
            var finalList = _uowPatientOpenVisit.GetDbContext().ViewPatientOpenVisitLists.Where(x=>x.IsSendToCdc == true)
               .WhereIf(!string.IsNullOrEmpty(filter.FullName), x => x.PatientName!.ToLower().StartsWith(filter.FullName!))
               //.WhereIf(!string.IsNullOrEmpty(filter.TokenNo), x => x.TokenNo == filter.TokenNo!.PadLeft(4, '0'))
               .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.Cnic == filter.Cnic)
               .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.PatientMobileNo!.ToLower().StartsWith(filter.MobileNo!))
               .WhereIf(!string.IsNullOrEmpty(filter.Mrno), x => x.MrNo!.ToLower().StartsWith(filter.Mrno!))
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
               //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
               //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
               .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
               .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)

               .OrderByDescending(x => x.CreatedOn)
               .Select(x =>
               new ViewCdcDto
               {
                   PatientVisitId = x.PatientVisitId,
                   PatientId = x.PatientId,
                   FullName = x.PatientName,
                   MobileNo = x.PatientMobileNo,
                   PatientProvinceId = x.PatientProvinceId,
                   Mrno = x.MrNo,
                   Cnic = x.Cnic,
                   VisitDate = x.VisitDate,
                   CreatedBy = x.CreatedBy!,
                   CreatedOn = x.CreatedOn,
                   UpdatedBy = x.UpdatedBy!,
                   UpdatedOn = x.UpdatedOn,
                   DepartmentId = x.DepartementLookupId,
                   Department = x.Department,
                   Section = x.Section,
                   SectionId = x.SectionLookupId,
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   Gender = x.Gender,
                   Age = x.Age,
                   IsDischarge = x.IsDischarge,
                   HealthFacilityName = x.HealthFacilityName,
                   HealthFacilityId = x.HealthFacilityId,
                   TokenNo = x.TokenNo,
               });

            var pagedList = await PagedListDto<ViewCdcDto>.ToPagedListAsync(
                   finalList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewCdcDto>
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

       

        #endregion
    }
}
