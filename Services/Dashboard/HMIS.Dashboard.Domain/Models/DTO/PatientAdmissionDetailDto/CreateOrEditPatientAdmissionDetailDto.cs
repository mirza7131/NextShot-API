namespace HMIS.Dashboard.Domain.Models.DTO.PatientAdmissionDetailDto
{
    public class CreateOrEditPatientAdmissionDetailDto
    {
        public Guid? PatientAdmissionDetailId { get; set; }

        public bool? ShiftBack { get; set; } = false;
            
        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public int? ShiftedFromDepartmentLookupId { get; set; }

        public int? ShiftedFromSectionLookupId { get; set; }

        public int? DepartmentLookupId { get; set; }

        public int? SectionLookupId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public string? ReasonOfShifting { get; set; }

        public string? ShiftedBy { get; set; }

        public DateTime? ShiftedOn { get; set; }

        public string? AdmittedBy { get; set; }

        public DateTime? AdmittedInSpeciality { get; set; }

        public Guid? PatientLevelProfileId { get; set; }

        public bool? IsVantilated { get; set; }

        public bool? IsDischarge { get; set; }

        public DateTime? PatientAdmittedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
