using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HfLabTestConfigDto
{
    public class CreateOrEditHfLabTestConfigListDto
    {
        public CreateOrEditHfLabTestConfigListDto()
        {
            HfLabTestConfigList = new List<CreateOrEditHfLabTestConfigDto>();
        }
        public int HealthFacilityId { get; set; }
        
        public virtual ICollection<CreateOrEditHfLabTestConfigDto> HfLabTestConfigList { get; set; } 

    }

    public class CreateOrEditHfLabTestConfigDto
    {
        public Guid? HfLabTestConfigId { get; set; }

        public int HealthFacilityId { get; set; }

        public int LabTestId { get; set; }

        public bool? IsPerformedPrivately { get; set; }

        public Guid? LabDepartmentProfileId { get; set; }

        public bool IsActive { get; set; }
    }
}
