using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseTemplateDto
{
    public class ViewPatientDiagnoseTemplateDto
    {
        public Guid PatientDiagnoseTemplateId { get; set; }

        public Guid UserId { get; set; }

        public string? Name { get; set; }

        public string? Json { get; set; }

        public bool? IsActive { get; set; }
    }
}
