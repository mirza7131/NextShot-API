using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseDiseaseDto
{
    public class ViewPatientDiagnoseDiseaseDto
    {
        public Guid PatientDiagnoseDiseaseId { get; set; }

        public Guid PatientDiagnoseId { get; set; }

        public Guid DiseaseProfileId { get; set; }

        public Guid? PatientId { get; set; }

        public bool? IsActive { get; set; }
    }
}
