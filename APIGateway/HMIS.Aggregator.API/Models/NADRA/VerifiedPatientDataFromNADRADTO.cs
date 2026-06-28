using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models.NADRA
{
    public class VerifiedPatientDataFromNADRADTO
    {
        public string? transactionId { get;set; }
        public string? requestId { get;set; }
        public string? citizenNumber { get;set; }
        public string? name { get;set; }
        public string? currentAddress { get;set; }
        public string? permanentAddress { get;set; }
        public string? code { get;set; }
        public string? message { get;set; }
    }
}
