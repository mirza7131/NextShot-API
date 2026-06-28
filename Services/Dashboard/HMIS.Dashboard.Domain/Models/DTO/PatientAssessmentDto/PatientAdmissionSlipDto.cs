using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientAssessmentDto
{
    public class PatientAdmissionSlipDto
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
        public int? Age { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? HealthFacilityName { get; set; }
        //public Guid? GenderProfileId { get; set; }
        //public Guid? RelationProfileId { get; set; }

        //public int? ProvinceId { get; set; }
        //public string? ProvinceName { get; set; }
        //public int? DivisionId { get; set; }
        //public string? DivisionName { get; set; }
        //public int? DistrictId { get; set; }
        //public string? DistrictName { get; set; }
        //public int? TehsilId { get; set; }
        //public string? TehsilName { get; set; }
        public string? PermanentAddress { get; set; }
        public int? BedNo { get; set; }
        //public string? DepartmentName { get; set; }
        //public string? SectionName { get; set; }

        public DateTime? VisitDate { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsAdmittedInIPD { get; set; }
        public bool? IsReferredIpd { get; set; }
        public int? IpdDepartmentLookupId { get; set; }
        public string? IpdDepartmentLookupName { get; set; }
        public int? IpdSectionLookupId { get; set; }
        public string? IpdSectionLookupName { get; set; }
        public Guid? IpdReferredBy { get; set; }
        public string? IpdReferredByName { get; set; }
        public int? IpdReferredByDepartmentLookupId { get; set; }
        public string? IpdReferredByDepartmentLookupName { get; set; }
        public int? IpdReferredBySectionLookupId { get; set; }
        public string? IpdReferredBySectionLookupName { get; set; }
    }
}
