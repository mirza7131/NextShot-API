using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HfLabTestConfigDto
{
    public class SPHfLabTestConfigList
    {
        public int SrNo { get; set; }
        public int HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class SPHfLabTestConfigListTotalCount
    {
        public int TotalRecord { get; set; }
    }
}
