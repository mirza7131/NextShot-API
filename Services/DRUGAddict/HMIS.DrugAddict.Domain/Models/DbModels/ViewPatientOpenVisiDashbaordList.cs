using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class ViewPatientOpenVisiDashbaordList
{
    public int SscStatus { get; set; }

    public string? SscStatusReason { get; set; }

    public DateTime? SscStatusUpdatedOn { get; set; }

    public string? SscStatusUpdatedBy { get; set; }

    public string? SscStatusName { get; set; }

    public int ProvinceId { get; set; }

    public string? ProvinceName { get; set; }

    public int DivisionId { get; set; }

    public string? DivisionName { get; set; }

    public int DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public int TehsilId { get; set; }

    public string? TehsilName { get; set; }

    public int HealthFacilityId { get; set; }

    public string? HealthFacilityName { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientId { get; set; }

    public DateTime? VisitDate { get; set; }

    public string? TokenNo { get; set; }

    public string? PatientName { get; set; }

    public string? PatientMobileNo { get; set; }

    public string? MrNo { get; set; }

    public string Cnic { get; set; } = null!;

    public Guid? CreatedById { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public string? Section { get; set; }

    public string? Department { get; set; }

    public int PatientProvinceId { get; set; }

    public DateTime? Dob { get; set; }

    public string? PatientProvinceName { get; set; }

    public int? Age { get; set; }

    public Guid? GenderId { get; set; }

    public int? VisitNo { get; set; }

    public string Relation { get; set; } = null!;

    public DateTime? PatientCreatedOn { get; set; }

    public bool? IsDischarge { get; set; }

    public bool? IsSscClaimed { get; set; }

    public DateTime? SscClaimedDate { get; set; }

    public string Gender { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public Guid? ReasonIfSscNotClaimed { get; set; }

    public string? SscNotConfirmReason { get; set; }

    public bool? IsEligibleForSsc { get; set; }

    public string? SscNumber { get; set; }

    public string? SscNotEligibleReason { get; set; }

    public bool? IsVitalSkip { get; set; }
}
