using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HMIS.Dashboard.Domain.Models.DTO.HealthFacilityDto
{
    public class ViewHealthFacilityDto
    {
        public int HealthFacilityId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public string? HealthFacilityTypeCode { get; set; }

        public int? HealthFacilityTypeId { get; set; }

        public int? ProvinceId { get; set; }

        public string? ProvinceCode { get; set; }

        public int? DivisionId { get; set; }

        public string? DivisionCode { get; set; }

        public int? DistrictId { get; set; }

        public string? DistrictCode { get; set; }

        public int? TehsilId { get; set; }

        public string? TehsilCode { get; set; }

        public int? UnionCouncilId { get; set; }

        public string? UnionCouncilCode { get; set; }

        public bool IsActive { get; set; }


    }
}
