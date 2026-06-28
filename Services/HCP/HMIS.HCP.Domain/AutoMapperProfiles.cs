using HMIS.HCP.Domain.Models.DbModels;
using HMIS.HCP.Domain.Models.DTO;
using HMIS.Patient.Domain.Models.DTO.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using autoMapper = AutoMapper;

namespace HMIS.HCP.Domain
{
    internal class AutoMapperProfiles : autoMapper.Profile
    {
        #region Patient Assessment
        public class PatientAssessmentProfile : autoMapper.Profile
        {
            public PatientAssessmentProfile()
            {
                CreateMap<PatientAssessment, CreateOrEditPatientAssessmentDto>().ReverseMap();

            }

        }
        #endregion

        #region Patient Screening
        public class PatientScreeningProfile : autoMapper.Profile
        {
            public PatientScreeningProfile()
            {
                CreateMap<PatientScreening, CreateOrEditPatientScreeningDto>().ReverseMap();
                CreateMap<CallDetail, CreateOrEditCallDto>().ReverseMap();

            }

        }
        #endregion
    }
}
