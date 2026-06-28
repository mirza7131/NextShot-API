using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.HcpPatientDtos
{
    public class RecommendedLabTestToPatientDto
    {
        public string? LabTestName { get; set; }
        public int? LabTestId { get; set; }
        public bool? IsReportGenerated { get; set; }
        public bool? IsSampleCollected { get; set; }
        public bool? IsSampleRejected { get; set; }
        public string? TestName { get; set; }
        public string? TestUnit { get; set; }
        public string? ShortName { get; set; }
        public string? Result { get; set; }
    }
}
