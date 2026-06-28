using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class ViewPatientOpenVisitDetail
{
    public string? FullName { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public DateTime? VisitDate { get; set; }

    public DateTime? Dob { get; set; }

    public decimal? Age { get; set; }

    public string Gender { get; set; } = null!;

    public int? ChartPieSequenceNo { get; set; }

    public Guid GenderId { get; set; }

    public string? VisitHf { get; set; }

    public string? PatientHf { get; set; }

    public string Relation { get; set; } = null!;

    public string? PatientDivisionName { get; set; }

    public string? PatientProvinceName { get; set; }

    public string? PatientDistrictName { get; set; }

    public string? PatientTehsilName { get; set; }

    public DateTime? PatientCreatedOn { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? HealthFacilityProvinceId { get; set; }

    public int? HealthFacilityDivisionId { get; set; }

    public int? HealthFacilityDistrictId { get; set; }

    public int? HealthFacilityTehsilId { get; set; }

    public int HealthFacilityId { get; set; }

    public Guid? PatientVisitCreatedBy { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MobileNo { get; set; }

    public string? Mrno { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientOpenVisitId { get; set; }

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public string? PatientVisitCreatedByName { get; set; }

    public string? PatientVisitCreatedByCnic { get; set; }

    public string? PatientVisitCreatedByDesignation { get; set; }

    public int PatientProvinceId { get; set; }

    public bool IsFromPmis { get; set; }

    public bool? IsVisitClosed { get; set; }

    public bool? IsFilterClinic { get; set; }

    public int? ConsultantSectionLookupId { get; set; }

    public bool? IsConsultant { get; set; }

    public string? SectionName { get; set; }

    public string? CurrentStation { get; set; }

    public int? VisitNo { get; set; }

    public bool? IsSscClaimed { get; set; }

    public bool? IsEligibleForSsc { get; set; }

    public Guid? ReasonIfNotEligibleForSsc { get; set; }

    public Guid? ReasonIfSscNotClaimed { get; set; }
}
