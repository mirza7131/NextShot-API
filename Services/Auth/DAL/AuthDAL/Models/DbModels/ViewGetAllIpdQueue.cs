using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class ViewGetAllIpdQueue
{
    public Guid PatientOpenVisitId { get; set; }

    public Guid? PatientId { get; set; }

    public string? TokenNo { get; set; }

    public int? HealthFacilityId { get; set; }

    public int? DepartementLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public bool? IsDischarge { get; set; }

    public bool? IsOccupied { get; set; }

    public Guid? OccupiedBy { get; set; }

    public string? BedNo { get; set; }

    public string? DepartmentName { get; set; }

    public string? SectionName { get; set; }

    public string? Mrno { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MobileNo { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }
}
