using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class ViewHealthFacility
{
    public int Id { get; set; }

    public string? DivisionCode { get; set; }

    public string DivisionName { get; set; } = null!;

    public string? DistrictCode { get; set; }

    public string DistrictName { get; set; } = null!;

    public string? TehsilCode { get; set; }

    public string TehsilName { get; set; } = null!;

    public string Hfmiscode { get; set; } = null!;

    public string HealthFacilityName { get; set; } = null!;

    public string ModeName { get; set; } = null!;

    public string Lvl { get; set; } = null!;

    public int? ZoneId { get; set; }
}
