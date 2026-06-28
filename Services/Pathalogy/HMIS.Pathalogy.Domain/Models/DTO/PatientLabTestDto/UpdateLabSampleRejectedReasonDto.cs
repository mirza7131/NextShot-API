using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class UpdateLabSampleRejectedReasonDto
    {
        public Guid PatientLabTestId { get; set; }
        public string SampleRejectedReason { get; set; }
        public string SampleRejectedReasonName { get; set; }
        public bool IsExternal { get; set; } = false;
        public bool IsPreGeneratedBarcode { get; set; } = false;
        public string? PreGeneratedBarcode { get; set; }

    }
}
