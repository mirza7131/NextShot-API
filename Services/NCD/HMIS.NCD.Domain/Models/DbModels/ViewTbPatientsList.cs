using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class ViewTbPatientsList
{
    public Guid PatientId { get; set; }

    public string? Mrno { get; set; }

    public string? FullName { get; set; }

    public string Cnic { get; set; } = null!;

    public int? Age { get; set; }

    public string? MobileNo { get; set; }

    public string? FormType { get; set; }

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public Guid? GenderId { get; set; }

    public int? ProvinceId { get; set; }

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int TehsilId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? CreatedById { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }
}
