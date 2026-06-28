using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class ViewTbissuedMedicine
{
    public Guid PatientVisitId { get; set; }

    public string? FullName { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MobileNo { get; set; }

    public string? Mrno { get; set; }

    public int ProvinceId { get; set; }

    public int DivisionId { get; set; }

    public string? DivisionName { get; set; }

    public int DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public int TehsilId { get; set; }

    public string? TehsilName { get; set; }

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public int HealthFacilityId { get; set; }

    public string? HealthFacilityName { get; set; }

    public DateTime? CreatedOn { get; set; }
}
