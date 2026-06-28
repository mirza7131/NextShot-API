using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class PatientDiagnoseDto
    {
        public Guid PatientDiagnoseId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public string? FormType { get; set; }

        public int? DocDepartmentLookupId { get; set; }

        public int? DocSectionLookupId { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public string? ReferredBy { get; set; }
        public string? ReferredTo { get; set; }
        public string? ReferredDate { get; set; }
        public string? TreatmentOutCome { get; set; }
        public string? DiagnosticDisease { get; set; }
        public bool IsFollowUp { get; set; } = false;
        public bool IsPatientCounciled { get; set; } = false;
    }
}
