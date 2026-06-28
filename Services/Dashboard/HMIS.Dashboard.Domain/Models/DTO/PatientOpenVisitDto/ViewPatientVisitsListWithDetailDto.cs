using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientOpenVisitDto
{
    public class ViewPatientVisitsListWithDetailDto
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
        public bool? IsSscClaimed { get; set; }
        public int? Age { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? SscNotConfirmReason { get; set; }
        public bool? IsEligibleForSsc { get; set; }
        public string? SscNumber { get; set; }
        public string? SscNotEligibleReason { get; set; }
        public DateTime? SscClaimedDate { get; set; }
        public bool? IsVitalSkip { get; set; }
        public Guid? CurrentStationProfileId { get; set; }
        public int? SscStatus { get; set; }
        public string? SscStatusReason { get; set; }
        public string? SscStatusName { get; set; }
        public string? SscStatusUpdatedBy { get; set; }
        public DateTime? SscStatusUpdatedOn { get; set; }
        
    }
}
