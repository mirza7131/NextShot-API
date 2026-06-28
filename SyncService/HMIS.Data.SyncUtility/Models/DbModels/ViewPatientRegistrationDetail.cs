using System;
using System.Collections.Generic;

namespace HMIS.Data.SyncUtility.Models.DbModels;

public partial class ViewPatientRegistrationDetail
{
    public string? FullName { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public DateTime? Dob { get; set; }

    public decimal? Age { get; set; }

    public string Gender { get; set; } = null!;

    public string Relation { get; set; } = null!;

    public string? PatientDivisionName { get; set; }

    public string? PatientProvinceName { get; set; }

    public string? PatientDistrictName { get; set; }

    public string? PatientTehsilName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? HealthFacilityProvinceId { get; set; }

    public int? HealthFacilityDivisionId { get; set; }

    public int? HealthFacilityDistrictId { get; set; }

    public int? HealthFacilityTehsilId { get; set; }

    public Guid? PatientCreatedBy { get; set; }

    public int PatientProvinceId { get; set; }

    public int? VisitNo { get; set; }

    public int HealthFacilityId { get; set; }

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }
}
