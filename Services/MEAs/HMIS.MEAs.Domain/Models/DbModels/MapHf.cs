using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class MapHf
{
    public int Id { get; set; }

    public string? Hfid { get; set; }

    public string? DivisionName { get; set; }

    public string? DistrictName { get; set; }

    public string? TehilName { get; set; }

    public string? HealthFacilityName { get; set; }

    public string? ModeName { get; set; }

    public string? UpdatedModeName { get; set; }

    public string? CurrentZone { get; set; }

    public string? FinalZone { get; set; }
}
