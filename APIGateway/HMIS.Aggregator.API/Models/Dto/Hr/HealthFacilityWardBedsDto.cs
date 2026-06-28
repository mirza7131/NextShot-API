using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models.Dto.Hr
{
    public class HealthFacilityWardBedsDto
    {
        public int? id { get; set; }
        public long? healthFacility_Id { get; set; }
        public string? healthFacilityName { get; set; }
        
        public long? ward_Id { get; set; }
        public string? wardName { get; set; }
        public long? sanctioned { get; set; }

        public long? user_Id { get; set; }

        public bool isActive { get; set; } = false;
        public DateTime? datetime { get; set; }
    }
}
