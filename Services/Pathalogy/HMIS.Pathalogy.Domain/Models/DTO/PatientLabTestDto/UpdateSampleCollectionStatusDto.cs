using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class UpdateSampleCollectionStatusDto
    {
        public Guid PatientLabTestId { get; set; }
        public string? SampleTransportMode { get; set; }
        public bool IsExternal { get; set; } = false;
        public bool IsPreGeneratedBarcode { get; set; } = false;
        public string? PreGeneratedBarcode { get; set; }

        public SampleTransportByLHWDto? sampleTransportByLHW { get; set; }

    }


    public class SampleTransportByLHWDto
    {
        public string NameOfLHW {get; set;}
        public string ContactOfLHW {get; set;}
        public string CNICOfLHW {get; set;}
        public string CatchmentAreaOfLHW {get; set;}
        public string NameOfLHS {get; set;}
        public string ContactOfLHS {get; set;}
        public string CNICOfLHS { get; set; }
    }
}
