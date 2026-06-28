using HMIS.DrugAddict.Domain.Models.DbModels;
using HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto;
using HMIS.DrugAddict.Domain.Models.Dto.TestDto;
using HMIS.DrugAddict.Domain.Models.DTO.SocialWelfareFormDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using autoMapper = AutoMapper;

namespace HMIS.DrugAddict.Domain
{
    public class AutoMapperProfiles : autoMapper.Profile
    {
        #region UMS
        public class TestsssProfile : autoMapper.Profile
        {
            public TestsssProfile()
            {
                CreateMap<Testsss, CreateOrEditTestsssDto>().ReverseMap();
         
            }

        }
        public class SocialWelfareFormProfile : autoMapper.Profile
        {
            public SocialWelfareFormProfile()
            {
                CreateMap<SocialWelfareForm, CreateOrEditSocialWelfareFormDto>().ReverseMap();
                CreateMap<SocialWelfareAssignDoctor, SocaialWelfareAssignDoctorDto>().ReverseMap();
                CreateMap<ViewSocialWellfareDeputyDirector, ViewSocialWelfareDeputyDirectorDto>().ReverseMap();
                CreateMap<ViewSocialWellfareDeputyDirector, PatientsSingleSessionDetailDto>().ReverseMap();
                CreateMap<SocialWelfareTaskPerformedByCd, CreateOrEditSocialWelfareTaskPerformedByCDDto>().ReverseMap();
                CreateMap<SocialWelfareTaskPerformedByCd, SocialWelfareTaskPerformedByCdDto>().ReverseMap();
            }

        }
        #endregion
    }
}
