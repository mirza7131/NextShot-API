using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.DbModels;

public partial class ViewPatientOpenVisitDetail
{
    public string? FullName { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public DateTime? VisitDate { get; set; }

    public DateTime? Dob { get; set; }

    public decimal? Age { get; set; }

    public string Gender { get; set; } = null!;

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

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public string? PatientVisitCreatedByName { get; set; }

    public int PatientProvinceId { get; set; }

    public bool IsFromPmis { get; set; }

    public bool? IsFilterClinic { get; set; }

    public int? ConsultantSectionLookupId { get; set; }

    public bool? IsConsultant { get; set; }

    public string? SectionName { get; set; }

    public string? CurrentStation { get; set; }

    public int? VisitNo { get; set; }
}
