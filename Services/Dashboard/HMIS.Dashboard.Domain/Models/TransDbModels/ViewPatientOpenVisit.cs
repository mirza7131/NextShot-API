using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class ViewPatientOpenVisit
{
    public string? FullName { get; set; }

    public int? Age { get; set; }

    public DateTime? Dob { get; set; }

    public string Cnic { get; set; } = null!;

    public string? Domicile { get; set; }

    public string? Email { get; set; }

    public string? GuardianName { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? VisitDate { get; set; }

    public string? Name { get; set; }
}
