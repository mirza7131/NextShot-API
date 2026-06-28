using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using HMIS.DrugAddict.Domain.Models.DbModels;
using HMIS.DrugAddict.Domain.Models.Dto.ExternalApisDto;
using HMIS.DrugAddict.Domain.Models.Dto.FilterDto;
using HMIS.DrugAddict.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CommonMessages;
using Azure.Core;
using HMIS.DrugAddict.Domain.Models.Dto.PaginationDto;

namespace HMIS.DrugAddict.Service
{
    public class OutSourceApisService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<PatientDiagnosisRecord> _uowPatientDiagnosisRecord;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor

        public OutSourceApisService(
            TokenService tokenService, 
            IMapper mapper,
            UnitOfWork<PatientDiagnosisRecord> uowPatientDiagnosisRecord,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientDiagnosisRecord = uowPatientDiagnosisRecord;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region CUD Operations


        #endregion

        #region Read Operations


        public async Task<ViewPagerDto<ViewDrugAddictPatientVisitsDto>> GetDrugAddictPatientVisits(ExternalApiFilterDto filter)
        {
            var list = _uowPatientDiagnosisRecord.GetDbContext().ViewDrugAddictsPatientVisits
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.AdmissionDate!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.AdmissionDate!.Value.Date <= filter.EndDate!.Value.Date);

            IQueryable<ViewDrugAddictPatientVisitsDto> IQueryableList = list.Select(x => new ViewDrugAddictPatientVisitsDto
            {
                PatientId = x.PatientId,
                PatientVisitId = x.PatientOpenVisitId,
                FullName = x.FullName,
                Cnic = x.Cnic,
                Addicted = x.Addicted,
                GuardianName = x.GuardianName,
                Age = x.Age,
                Dob = x.Dob,
                Gender = x.Gender,
                ParmanentAddress = x.ParmanentAddress,
                District = x.District,
                AdmissionDate = x.AdmissionDate,
                DischargeDate = x.DischargeDate,
                TreatmentStatus = x.TreatmentStatus,
                Rehabilitation = x.Rehabilitation,
                HealthFacilityTypeId = x.HealthFacilityTypeId,
                HealthFacilityTypeName = x.Name,
                HealthFacility = x.HealthFacility,
            }) ;
            var pagedList = await PagedListDto<ViewDrugAddictPatientVisitsDto>.ToPagedListAsync(
                 IQueryableList,
                 filter.PageNumber,
                 filter.PageSize
                 );


            var responseObject = new ViewPagerDto<ViewDrugAddictPatientVisitsDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList,
            };

            //var remoteIpAddress = HttpContext.Current.Request.UserHostAddress; ;
            var remoteIpAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            var _uowOutSourceSystemLog = new UnitOfWork<OutSourceSystemLog>(_uowPatientDiagnosisRecord.GetDbContext());

            OutSourceSystemLog objOutSourceSystemLog = new OutSourceSystemLog();
            objOutSourceSystemLog.SourceSystemId = await _uowPatientDiagnosisRecord.GetDbContext().SourceSystems.Where(x => x.ShortName == CommonStringConstant.PITBDrugAddict).Select(x => x.SourceSystemId).FirstOrDefaultAsync();
            objOutSourceSystemLog.UserSystemIp = remoteIpAddress;
            objOutSourceSystemLog.TargetedApiUrl = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host.Value.ToString()}{_httpContextAccessor.HttpContext?.Request.PathBase.Value.ToString()}{_httpContextAccessor.HttpContext?.Request.Path.ToUriComponent()}"; ;// + "api/OutSourceApis/GetDrugAddictPatientVisits?StartDate=2023-07-01&EndDate=2023-07-21"; //CommonStringConstant.DrugAddictUrl + "api/OutSourceApis/GetDrugAddictPatientVisits?StartDate=2023-07-01&EndDate=2023-07-21";
            objOutSourceSystemLog.CreatedBy = _tokenService.GetUserId(); ;
            objOutSourceSystemLog.InputParameters = JsonConvert.SerializeObject(filter);
            objOutSourceSystemLog.CreatedOn = DateTime.Now;
            objOutSourceSystemLog.OutSourceSystemLogId = Guid.NewGuid();
            await _uowOutSourceSystemLog.Repository.Insert(objOutSourceSystemLog);
            await _uowOutSourceSystemLog.Save();


            return responseObject;

        }
        #endregion

        #region Helper Methods



        #endregion

    }
}
