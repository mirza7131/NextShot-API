using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Aggregator.API.Models
{
    public class NadraVerificationDTO
    {
        public string? Id { get; set; }
        public string? LocationId { get; set; }
        public string? userId { get; set; } = "";
        public string? RequestId { get; set; }
        public string? RequestCode { get; set; } // Dead Body or Mortuary
        public string? Gender { get; set; } = "0";
        public string? fingerprintFormat { get; set; } = "2";
        public string? f1 { get; set; } = "";
        public string? f2{ get; set; }="";
        public string? f3{ get; set; }="";
        public string? f4{ get; set; }="";
        public string? f5{ get; set; }="";
        public string? f6{ get; set; }="";
        public string? f7{ get; set; }="";
        public string? f8{ get; set; }="";
        public string? f9 { get; set; } = "";
        public string? f10 { get; set; } = "";
        public string? Photo { get; set; } = "";
    }
}
