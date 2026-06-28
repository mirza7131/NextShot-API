using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models.ResponseModels
{
    public class ResponseProfileDTO
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public ICollection<ProfileDTO> Data { get; set; }
    }
}
