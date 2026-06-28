using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.DTO.HealthFacility
{
    public class ViewHealthFacilityDto
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
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? HealthFacilityTypeCode { get; set; }
        public int? HealthFacilityTypeId { get; set; }
    }
}