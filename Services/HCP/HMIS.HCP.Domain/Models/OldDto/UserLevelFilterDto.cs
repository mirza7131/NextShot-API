using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HCP.Domain.Models.OldDto
{
    public class UserLevelFilterDto
    {
        public int? ProvinceId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
        public int? HealthFacilityId { get; set; }
        public string? HealthFacilityCode { get; set; }
        public string? DivisionCode { get; set; }
        public string? DistrictCode { get; set; }
        public string? TehsilCode { get; set; }
    }
}
