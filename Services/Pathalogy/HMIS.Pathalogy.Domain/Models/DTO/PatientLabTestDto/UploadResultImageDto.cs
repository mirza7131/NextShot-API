using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class UploadResultImageDto
    {
        public Guid PatientLabTestId { get; set; }
        public string? ResultImageLink { get; set; }
    }
}
