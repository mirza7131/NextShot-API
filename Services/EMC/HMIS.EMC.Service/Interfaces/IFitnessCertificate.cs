using HMIS.EMC.Domain.Models.DbModels;
using HMIS.EMC.Domain.Models.Dto;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using HMIS.EMC.Domain.Models.Dto.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Service.Interfaces
{
    public interface IFitnessCertificate
    {
        Task<CreateOrEditFitnessCertificateDto> CreateOrEdit(CreateOrEditFitnessCertificateDto input);
        Task<ViewPagerDto<GetAllFitnessCertificateDto>> GetAllFitnessCertificatePatients(SearchFilterDto? filter);
        //Task<List<GetSingleFitnessCertificateDto>> GetSingleFitnessCertificatePatientInfo(Guid PatientVisitId);
        Task<GetSingleFitnessCertificateDtoWithQuestions> GetSingleFitnessCertificatePatientInfo(Guid PatientVisitId);
        Task<List<ViewGetAllIpsychologicalAssessmentQuestion>> GetTenPsychologicalAssessmentQuestions();
    }
}