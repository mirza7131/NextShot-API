using HMIS.MIMS.Domain.Models.DbModels;
using HMIS.MIMS.Domain.Models.DTO.IndentDetailDto;
using HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO;
using HMIS.MIMS.Domain.Models.DTO.InventoryMasterDto;
using HMIS.MIMS.Domain.Models.DTO.MimsLookupsDto;
using autoMapper = AutoMapper;

namespace HMIS.MIMS.Domain
{
    public class AutoMapperProfiles
    {
        public class IndentMasterProfile : autoMapper.Profile
        {
            public IndentMasterProfile()
            {
                CreateMap<IndentMaster, CreateOrEditIndentMasterDto>().ReverseMap();
                CreateMap<IndentMaster, ViewIndentMasterDto>().ReverseMap();
                CreateMap<IndentDetailDTO, MedicineIndentDetailDTO>().ReverseMap();
            }
        }

        public class IndentDetailProfile : autoMapper.Profile
        {
            public IndentDetailProfile()
            {
                CreateMap<IndentDetail, CreateOrEditIndentDetailDto>().ReverseMap();
                CreateMap<IndentDetail, ViewIndentDetailDTO>().ReverseMap();

            }
        }

        public class MimsBranchProfile : autoMapper.Profile
        {
            public MimsBranchProfile()
            {
                CreateMap<MimsBranch, ViewMimsBranchDto>().ReverseMap();
            }
        } 
        
        public class InventoryMasterProfile : autoMapper.Profile
        {
            public InventoryMasterProfile()
            {
                CreateMap<InventoryMaster, ViewInventoryMasterDto>().ReverseMap();
            }
        }

    }
}
