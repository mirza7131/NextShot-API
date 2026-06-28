using HMIS.Dashboard.Domain.Models.DbModels;
using HMIS.Dashboard.Domain.Models.DTO.Patient;
using HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseDiseaseDto;
using HMIS.Dashboard.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Dashboard.Domain.Models.DTO.PatientPrescriptionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDiagnoseDto
{
    public class ViewPatientDiagnoseDto
    {
        public Guid PatientDiagnoseId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public string? PresentComplaints { get; set; }
        public string? Examination { get; set; }
        public string? PatientMedicalHistory { get; set; }
        public string? AdviseGiven { get; set; }
        public DateTime? FollowupDate { get; set; }
        public Guid? DiagnosedBy { get; set; }
        public bool IsActive { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Mrno { get; set; }
        public string? Cnic { get; set; }
        public int? VisitNo { get; set; }
        public DateTime? VisitDate { get; set; }
        public bool? IsDischarge { get; set; }
        public string? TokenNo { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public bool? HasLabTest { get; set; }
        public virtual ViewPatientDto? Patient { get; set; }
        public virtual ViewPatientOpenVisitDto? PatientVisit { get; set; }
        public virtual List<ViewPatientDiagnoseDiseaseDto>? PatientDiagnoseDiseases { get; set; }
        public virtual List<ViewPatientPrescriptionDto>? PatientPrescriptions { get; set; }

    }
}
