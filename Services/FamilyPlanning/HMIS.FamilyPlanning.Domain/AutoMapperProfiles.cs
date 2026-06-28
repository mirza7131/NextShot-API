using HMIS.FamilyPlanning.Domain.Models.DbModels;
using HMIS.FamilyPlanning.Domain.Models.DTO;
using autoMapper = AutoMapper;

namespace HMIS.FamilyPlanning.Domain
{
	public class AutoMapperProfiles
	{
		public class ClientHistoryProfile : autoMapper.Profile
		{
			public ClientHistoryProfile()
			{
				CreateMap<RegistrationDetail, RegistrationDetailDTO>().ReverseMap();
				CreateMap<PastHistory, PastHistoryDTO>().ReverseMap();
				CreateMap<MedicalHistory, MedicalHistoryDTO>().ReverseMap();
				CreateMap<SurgicalHistory, SurgicalHistoryDTO>().ReverseMap();
			}
		}



		public class CounslingAndProvisionProfile : autoMapper.Profile
		{
			public CounslingAndProvisionProfile()
			{
				CreateMap<CounslingAndProvision, CounsellingAndProvisionDTO>().ReverseMap();
			}
		}
		public class ClientFollowupProfile : autoMapper.Profile
		{
			public ClientFollowupProfile()
			{
				CreateMap<ClientFollowup, ClientFollowupDTO>().ReverseMap();
			}
		}
	}
}
