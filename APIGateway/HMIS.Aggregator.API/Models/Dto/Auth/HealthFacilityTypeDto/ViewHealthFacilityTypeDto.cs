using CommonMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models.Dto.Auth
{
    public class ViewHealthFacilityTypeDto
    {
        public int HealthFacilityTypeId { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }

        public int? HealthFacilityCategoryId { get; set; }

        public bool IsActive { get; set; }
    }
    public class HmisHealthFacilityTypeResponseDTO
    {
        public HttpStatusCode statusCode { get; set; }
        public bool status { get; set; } = true;
        public string message { get; set; } = CommonMessageConstant.Read;
        public List<ViewHealthFacilityTypeDto> data { get; set; }
    }
}
