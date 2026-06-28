using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
    public class ViewPatientGlobalQueDto
    {
        public ViewPatientGlobalQueDto()
        {
                AttendedDiagnoseLists = new List<ViewAttendedDiagnoseListForGlobalQueDto>();
        }
        public Guid? PatientVisitId { get; set; }
        public Guid? PatientId { get; set; }
        public string? Mrno { get; set; }
        public string? MobileNo { get; set; }
        public string? CNIC { get; set; }
        public string? TokenNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Relation { get; set; }
        public bool? SelfOccupied { get; set; }
        public bool? IsOccupied { get; set; }
        public Guid? OccupiedBy { get; set; }
        public string? OccupiedByName { get; set; }
        public bool? IsVisitClose { get; set; }
        public int? DepartmentLookupId { get; set; }
        public int? SectionLookupId { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public bool? IsPickFromGlobalQueue { get; set; }
        public bool IsAlreadyAttended { get; set; }

        public bool IsSelfAttended { get; set; }
        public Guid? AttendedPatientDiagnoseId { get; set; }
        public ICollection<ViewAttendedDiagnoseListForGlobalQueDto> AttendedDiagnoseLists { get; set; }

    }

    public class ViewAttendedDiagnoseListForGlobalQueDto
    {
        public Guid? PatientDiagnoseId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public Guid? DiagnosedBy { get; set; }
        public string? DiagnosedByName { get; set; }
        public string? DiagnosedByDesignation { get; set; }
        public string? Speciality { get; set; }
        public string? FormType { get; set; }
        public DateTime? CreatedOn { get; set; }

    }
}
