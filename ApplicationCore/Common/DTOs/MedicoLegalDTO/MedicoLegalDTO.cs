using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDTOs.MedicoLegalDTO
{
    public class MedicoLegalDTO
    {
        public int srNo { get; set; }
        public string? hfmiscodenew { get; set; }
        public string? divisionCode { get; set; }
        public string? division { get; set; }
        public string? districtCode { get; set; }
        public string? district { get; set; }
        public string? tehsilCode { get; set; }
        public string? tehsil { get; set; }
        public string? healthFacility { get; set; }
        public string? hfTypeCode { get; set; }
        public string? hftype { get; set; }
        public int? mle { get; set; }
        public int? mleSv { get; set; }
        public int? postMortem { get; set; }
        public int? totalCases { get; set; }



        public class ResponseMedicoLegalDTO
        {
            public string err { get; set; }
            public string? message { get; set; }
            public List<MedicoLegalDTO> data { get; set; }

        }

    }
}
