using CommonMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace HMIS.Aggregator.API.Models.Dto.Auth
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

    public class HmisHealthFacilityPaginationResponseDTO
    {
        public HttpStatusCode statusCode { get; set; }
        public bool status { get; set; } = true;
        public string message { get; set; } = CommonMessageConstant.Read;
        public ViewHealthFacilityDto data { get; set; }
    }

    public class HmisHealthFacilityResponseDTO
    {
        public HttpStatusCode statusCode { get; set; }
        public bool status { get; set; } = true;
        public string message { get; set; } = CommonMessageConstant.Read;
        public List<ViewHealthFacilityDto> data { get; set; }
    }
}
