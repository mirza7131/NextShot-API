using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HCP.Domain.Models.DTO
{
    public class PatientSampleDataDto
    {
        public string TestType { get; set; }
        public string SampleNumber { get; set; }
        public string SampleID { get; set; }
    }
}
