using HMIS.NCD.Domain.Models.DbModels;
using HMIS.NCD.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using autoMapper = AutoMapper;

namespace HMIS.NCD.Domain
{
    public class AutoMapperProfiles : autoMapper.Profile
    {
        #region MyRegion
        public class AssessmentQaProfile : autoMapper.Profile
        {
            public AssessmentQaProfile()
            {
                CreateMap<NcdAssessmentAnswer, NcdAssessmentAnswersDto>().ReverseMap();
                CreateMap<NcdAssessmentAnswer, NcdAssessmentAnswersDtoForCopdAsthmaORDiabates>().ReverseMap();
                CreateMap<PatientFollowUp, CreateOrEditPatientFollowUpNcdClinicDto>().ReverseMap();
                CreateMap<PatientDiagnoseDisease, CreateOrEditPatientDiagnoseDiseses>().ReverseMap();
                CreateMap<PatientPrescription, CreateOrEditPatientPresCriptionDto>().ReverseMap();
                CreateMap<PatientDiagnose, PatientDiagnoseDto>().ReverseMap();
                CreateMap<DiseaseStatus, CreateOrEditPatientDiagnoseDiseses>().ReverseMap();
                CreateMap<PatientFamiliyHistoryDTO, ViewNcdPatientFamiliyHistory>().ReverseMap();
                CreateMap<PersonalHistoryDTO, PersonalHistory>().ReverseMap();
            }
        }


        public class PatientLabTestProfile : autoMapper.Profile
        {
            public PatientLabTestProfile()
            {
                CreateMap<PatientLabTest, CreateOrEditPatientLabTestDto>().ReverseMap();
            }
        }

        public class PatientLabTestDetailProfile : autoMapper.Profile
        {
            public PatientLabTestDetailProfile()
            {
                CreateMap<PatientLabTestDetail, CreateOrEditPatientLabTestDto>().ReverseMap();
                CreateMap<PatientLabTestDetail, LabTestDetail>().ReverseMap();
            }
        }
        public class WellWomanProfile : autoMapper.Profile
        {
            public WellWomanProfile()
            {
                CreateMap<BreastCancerPatientDetail, BreastCBCDTO>().ReverseMap();
                CreateMap<CervicalCancerPatientDetail, PatientRefer>().ReverseMap();
            }
        }
        #endregion
    }
}
