using System;
using System.Collections.Generic;

namespace HMIS.Pathalogy.Domain.Models.DbModels;

public partial class ViewDrugAddictPatientDisease
{
    public Guid PatientId { get; set; }

    public Guid PatientOpenVisitId { get; set; }

    public string? FullName { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MobileNo { get; set; }

    public string? Mrno { get; set; }

    public Guid? CreatedById { get; set; }

    public string? Name { get; set; }

    public int HealthFacilityId { get; set; }

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public int? ProvinceId { get; set; }

    public int? TehsilId { get; set; }

    public int? DistrictId { get; set; }

    public int? DivisionId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? ProfileName { get; set; }

    public string? ShortName { get; set; }
}
