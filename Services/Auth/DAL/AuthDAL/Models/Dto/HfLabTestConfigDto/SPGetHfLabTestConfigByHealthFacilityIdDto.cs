using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HfLabTestConfigDto
{
    public class SPGetHfLabTestConfigByHealthFacilityIdDto
    {
        public SPGetHfLabTestConfigByHealthFacilityIdDto()
        {
            HfLabTestConfigList = new List<HfLabTestConfigListDto>();
        }
        public int HealthFacilityId { get; set; }

        public virtual ICollection<HfLabTestConfigListDto> HfLabTestConfigList { get; set; }
    }

    //public class SPGetHfDataByHealthFacilityIdDto
    //{
    //    public int HealthFacilityId { get; set; }
    //    public string? HealthFacilityName { get; set; }
    //}

    public class HfLabTestConfigListDto
    {
        public Guid? HfLabTestConfigId { get; set; }

        public int HealthFacilityId { get; set; }

        public int LabTestId { get; set; }

        public string? Name { get; set; }

        //Lab Test Type ShortName
        public string? ShortName { get; set; }

        public string? DepartmentShortName { get; set; }
        public Guid? DepartmentProfileId { get; set; }
        public Guid? LabTestTypeProfileId { get; set; }

        public decimal? TestPrice { get; set; }
        public bool? IsPerformedPrivately { get; set; }
    }
}
