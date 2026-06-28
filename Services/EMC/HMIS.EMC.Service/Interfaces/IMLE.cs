using HMIS.EMC.Domain.Models.DbModels;
using HMIS.EMC.Domain.Models.Dto;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using HMIS.EMC.Domain.Models.Dto.PaginationDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Service.Interfaces
{
    public interface IMLE
    {
        Task<CreateOrEditMLEBasicInfoDto> CreateOrEdit(CreateOrEditMLEBasicInfoDto input);
        Task<CreateOrEditMLEExaminationDto> CreateOrEdit(CreateOrEditMLEExaminationDto input);
        Task<CreateOrEditMleReportDto> CreateOrEdit(CreateOrEditMleReportDto input);
        Task<ViewPagerDto<MLEAllPatientListDto>> GetAllMlePatients(SearchFilterDto? filter);
        Task<MlebasicInfo> GetSinglePatientBasicInfo(Guid PatientId);

        Task<List<MlePatientsDto>> GetSinglePatientMleInfo(Guid PatientId);
        Task<List<ViewMlcdoctorList>> GetAllMlcDoctors(int HealthFacilityId);
        Task<ViewPagerDto<ViewGetAllPatientThatAreNotCheckedYet>> GetAllMLCDoctorsByHealthFacilityId(SearchFilterDto? filter);
        Task<UpdateAssignDoctorDto> UpdateAssignDoctor(UpdateAssignDoctorDto input);

        Task<UpdateAssignDoctorDto> GetSingleMLCPatientsThatAreNotCheckedYet(Guid MlcId);
        Task<ViewPagerDto<ViewGetAllPatientForMlc>> GetAllMLCsByHealthFacilityId(SearchFilterDto filter);
    }
}
