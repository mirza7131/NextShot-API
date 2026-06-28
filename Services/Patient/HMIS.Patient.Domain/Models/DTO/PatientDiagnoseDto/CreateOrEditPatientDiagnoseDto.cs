using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDiseaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.Patient.Domain.Models.DTO.PatientPrescriptionDto;

namespace HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto
{
    public class CreateOrEditPatientDiagnoseDto
    {
        public CreateOrEditPatientDiagnoseDto()
        {
            PatientDiagnoseDiseases = new List<CreateOrEditPatientDiagnoseDiseaseDto>();
        } 
        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public string? PresentComplaints { get; set; }

        public string? Examination { get; set; }

        public string? PatientMedicalHistory { get; set; }

        public string? AdviseGiven { get; set; }

        public DateTime? FollowupDate { get; set; }

        public Guid? DiagnosedBy { get; set; }

        public bool? IsDischargeDiagnose { get; set; }

        public bool? IsDiagnoseExternally { get; set; }

        public Guid? SourceSystemId { get; set; }

        public string? SourceDoctorName { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<CreateOrEditPatientDiagnoseDiseaseDto> PatientDiagnoseDiseases { get; set; }

        public virtual ICollection<CreateOrEditPatientPrescriptionDto> PatientPrescriptions { get; set; }

    }
}
