using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientDto
{
    public class ViewPatientQueDto
    {
        public Guid? PatientVisitId { get; set; }
        public Guid? PatientId { get; set; }
        public string? Mrno { get; set; }
        public string? MobileNo { get; set; }
        public string? CNIC { get; set; }
        public string? TokenNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? PatientCondition { get; set; }
        public int? BedNo { get; set; }
        public string? VisitFor { get; set; }
        public bool IsAlreadyAttended { get; set; }
        public Guid? AttendedPatientDiagnoseId { get; set; }

        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }

    }

    public class ViewPatientDoctorQueDto
    {
        public ViewPatientDoctorQueDto()
        {
            AttendedDiagnoseLists = new List<ViewAttendedDiagnoseListDto>();
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
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? PatientCondition { get; set; }
        public int? BedNo { get; set; }
        public string? VisitFor { get; set; }
        public bool IsAlreadyAttended { get; set; }
        public bool IsSelfAttended { get; set; }
        public Guid? AttendedPatientDiagnoseId { get; set; }

        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }

        public ICollection<ViewAttendedDiagnoseListDto> AttendedDiagnoseLists { get; set; }

    }

    public class ViewAttendedDiagnoseListDto
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
