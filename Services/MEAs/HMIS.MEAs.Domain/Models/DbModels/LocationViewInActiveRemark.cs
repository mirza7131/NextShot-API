using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class LocationViewInActiveRemark
{
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

    public int? HfId { get; set; }

    public int Active { get; set; }

    public string Remarks { get; set; } = null!;

    public string InActiveTill { get; set; } = null!;
}
