using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.CdcDto
{
    public class ViewCdcDto
    {
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public string? TokenNo { get; set; }
        public int? VisitNo { get; set; }
        public string? Mrno { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Cnic { get; set; }
        public string? MobileNo { get; set; }
        public int? HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }
        public DateTime? VisitDate { get; set; }
        public bool? IsDischarge { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public int PatientProvinceId { get; set; }
        public int? DepartmentId { get; set; }
        public string? Department { get; set; }
        public int? SectionId { get; set; }
        public string? Section { get; set; }
        public string? FormType { get; set; }
        public Guid? PatientDiagnoseId { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
 
    }
}
