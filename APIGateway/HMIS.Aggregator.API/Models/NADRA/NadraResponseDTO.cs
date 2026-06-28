using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models.NADRA
{
    public class NadraResponseDTO
    {
        public string? transactionId { get; set; }
        public string? requestID { get; set; }
        public string? code { get; set; }
        public string? message { get; set; }
    }
}
