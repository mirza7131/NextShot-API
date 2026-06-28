using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDTOs.HealthCouncilDTO
{
    public class HealthCouncilDTO
    {

        public string? division { get; set; }
        public string? district { get; set; }
        public string? tehsil { get; set; }
        public string? hftypeCode { get; set; }
        public string? hftypeName { get; set; }
        public string? hfcode { get; set; }
        public string? hfname { get; set; }
        public decimal? totalAllocation { get; set; }
        public decimal? totalExpenditures { get; set; }
        public decimal? totalcurrentBalance { get; set; }

        public class ResponseHealthCouncilDTO
        {
            public string err { get; set; }
            public string? message { get; set; }
            public List<HealthCouncilDTO> data { get; set; }

        }

    }
}
