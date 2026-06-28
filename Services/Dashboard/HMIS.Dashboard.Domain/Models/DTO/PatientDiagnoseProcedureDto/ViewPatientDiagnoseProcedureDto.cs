using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseProcedureDto
{
    public class ViewPatientDiagnoseProcedureDto
    {
        public Guid? PatientDiagnoseProcedureId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public int? SectionProcedureId { get; set; }

        public bool? IsPerformed { get; set; }
        public byte? IsActive { get; set; }
        public Guid? RecommendBy { get; set; }

        public Guid? PerformedBy { get; set; }

        public string? Feedback { get; set; }
    }
}
