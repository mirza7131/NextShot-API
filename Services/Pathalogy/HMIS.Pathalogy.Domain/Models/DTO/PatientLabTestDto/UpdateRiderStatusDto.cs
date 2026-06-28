using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class UpdateRiderLabTestStatusDto
    {
        public Guid PatientLabTestId { get; set; }
        public byte Status { get; set; }
        public string? PreGeneratedBarcodeNo { get; set; }
    }
}
