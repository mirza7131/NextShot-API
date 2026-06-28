using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using autoMapper = AutoMapper;
using System.Threading.Tasks;
using HMIS.Pathalogy.Domain.Models.DTO.ViewPatientLabTestListDto;
using HMIS.Pathalogy.Domain.Models.DbModels;
using HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto;
using HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDetailDto;
using HMIS.Pathalogy.Domain.Models.DTO.ProfileTypeDto;
using HMIS.Pathalogy.Domain.Models.DTO.ProfileDto;
using HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDto;
using HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDetailDto;

namespace HMIS.Pathalogy.Domain
{
    public class AutoMapperProfiles
    {

        #region PatientLabTest

        public class PatientLabTestProfile : autoMapper.Profile
        {
            public PatientLabTestProfile()
            {
                CreateMap<PatientLabTest, CreateOrEditPatientLabTestDto>().ReverseMap();
                CreateMap<PatientLabTest, ViewPatientLabTestDto>().ReverseMap();
                CreateMap<PatientLabTest, UpdateLabTestResultDto>().ReverseMap();
                CreateMap<PatientLabTest, UploadResultImageDto>().ReverseMap();
                CreateMap<PatientLabTest, ViewPatientLabTestListDto>().ReverseMap();
                CreateMap<SampleTransportByLhw, SampleTransportByLHWDto>().ReverseMap();
                CreateMap<SPPatientLabTestListDto, ViewPatientLabTestListDto>().ReverseMap();
                CreateMap<SPPatientLabTestListDto, ViewPatientLabTestListDto>().ReverseMap();

            }
        }

        public class PatientLabTestDetailProfile : autoMapper.Profile
        {
            public PatientLabTestDetailProfile()
            {
                CreateMap<PatientLabTestDetail, CreateOrEditPatientLabTestDetailDto>().ReverseMap();
                CreateMap<PatientLabTestDetail, ViewPatientLabTestDetailDto>().ReverseMap();
            }
        }




        #endregion

        #region DB View
        public class ViewPatientLabTestListProfile : autoMapper.Profile
        {
            public ViewPatientLabTestListProfile()
            {
                CreateMap<ViewPatientLabTestListProfile, ViewPatientLabTestListDto>().ReverseMap();
            }
        }


        #endregion

        #region
        public class ProfileTypeProfile : autoMapper.Profile
        {
            public ProfileTypeProfile()
            {
                CreateMap<ProfileType, CreateOrEditProfileTypeDto>().ReverseMap();
                CreateMap<ProfileType, ViewProfileTypeDto>().ReverseMap();
            }
        }

        public class ProfileProfile : autoMapper.Profile
        {
            public ProfileProfile()
            {
                CreateMap<Profile, CreateOrEditProfileDto>().ReverseMap();
                CreateMap<Profile, ViewProfileDto>().ReverseMap();
            }
        }

        #endregion

        #region SampleConsignment
        public class SampleConsignmentProfile : autoMapper.Profile
        {
            public SampleConsignmentProfile()
            {
                CreateMap<SampleConsignment, CreateOrEditSampleConsignmentDto>().ReverseMap();
                CreateMap<SampleConsignment, ViewSampleConsignmentDto>().ReverseMap();
            }
        }

        public class SampleConsignmentDetailProfile : autoMapper.Profile
        {
            public SampleConsignmentDetailProfile()
            {
                CreateMap<SampleConsignmentDetail, CreateOrEditSampleConsignmentDetailDto>().ReverseMap();
                CreateMap<SampleConsignmentDetail, ViewSampleConsignmentDetailDto>().ReverseMap();
            }
        }

        #endregion

    }
}
