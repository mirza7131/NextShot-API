using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models.Dto.Hr
{
    public class ResponseHfWardBedsDto
    {
        public ResponseHfWardBedsDto()
        {
            list = new List<HealthFacilityWardBedsDto>();
        }
        public int? code { get; set; }
        public string? message { get; set; }
        public bool? success { get; set; }
        public bool? error { get; set; }
        public long? totalRecords { get; set; }
        public List<HealthFacilityWardBedsDto> list { get; set; }
        public object? data { get; set; }
        
    }
}
