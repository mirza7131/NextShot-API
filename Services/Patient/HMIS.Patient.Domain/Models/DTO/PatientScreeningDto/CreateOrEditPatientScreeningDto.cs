using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientScreeningDto
{
    public class CreateOrEditPatientScreeningDto
    {
        public Guid? PatientScreeningId { get; set; }

        public Guid? PatientId { get; set; }
        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public bool? IsPreviouslyDiagnosedHbv { get; set; }

        public bool? HasHbvpcrconfirmation { get; set; }

        public bool? IsPreviouslyDiagnosedHcv { get; set; }

        public bool? HasHcvpcrconfirmation { get; set; }

        public string? PatientType { get; set; }
        public Guid? PatientTypeProfileId { get; set; }

        public bool? IsDiagnosedHbvrepidKit { get; set; }

        public bool? IsDiagnosedHcvrepidKit { get; set; }
        public bool? IsDialysisPatient { get; set; }
    }
}
