using System.Text;
using System.Threading.Tasks;
using autoMapper = AutoMapper;
using HMIS.MEAs.Domain.Models.DbModels;
using DbModel = HMIS.MEAs.Domain.Models.DbModels;
using HMIS.MEAs.Domain.Models.DTO.UsersModel;

namespace HMIS.MEAs.Domain
{
    public class AutoMapperProfiles
    {
        public class UserProfile : autoMapper.Profile
        {
            public UserProfile()
            {
                //CreateMap<DbModel.User, RegisterDTO>().ReverseMap();
                CreateMap<DbModel.User, RegisterDTO>()
                    .ForMember(dest => dest.UserLocations, opt => opt.MapFrom(src => src.UserLocations))
                    .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles))
                    .ReverseMap();
                CreateMap<DbModel.UserLocation, UserLocationDto>()
                    .ReverseMap();
                CreateMap<DbModel.UserRole, UserRoleDto>()
                    .ReverseMap();
            }
        }
    }
}
