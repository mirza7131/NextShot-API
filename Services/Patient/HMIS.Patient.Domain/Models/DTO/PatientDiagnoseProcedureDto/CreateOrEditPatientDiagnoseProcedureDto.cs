using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDiagnoseProcedureDto
{
    public class CreateOrEditPatientDiagnoseProcedureDto
    {
        public Guid? PatientDiagnoseProcedureId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public int? SectionProcedureId { get; set; }
        public int? ProcedureFee { get; set; }

        public bool? IsActive { get; set; }

        public byte ActionTypeId { get; set; }

        public Guid? RecommendBy { get; set; }

        public Guid? PerformedBy { get; set; }

        public string? Feedback { get; set; }

        public bool? IsPerformed { get; set; }

        public string? ToothNumber { get; set; }

        public string? ToothPosition { get; set; }
        public Guid? AssistedBy { get; set; }

    }
}
