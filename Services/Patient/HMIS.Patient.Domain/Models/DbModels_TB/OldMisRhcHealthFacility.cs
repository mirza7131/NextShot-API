using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class OldMisRhcHealthFacility
{
    public int Id { get; set; }

    public string? HealthFacilityName { get; set; }

    public string? DivisionName { get; set; }

    public string? DistrictName { get; set; }

    public string? TehsilName { get; set; }

    public string? HfCategory { get; set; }

    public string? Identifier { get; set; }

    public string? Status { get; set; }
}
