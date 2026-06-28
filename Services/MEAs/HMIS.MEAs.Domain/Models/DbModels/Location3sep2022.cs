using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Location3sep2022
{
    public int LocationId { get; set; }

    public string? DivisionCode { get; set; }

    public string? DivisionName { get; set; }

    public string? DistrictCode { get; set; }

    public string? DistrictName { get; set; }

    public string? TehsilCode { get; set; }

    public string? TehsilName { get; set; }

    public string? Hfmiscode { get; set; }

    public string? HealthFacilityName { get; set; }

    public string? ModeName { get; set; }

    public string? Lvl { get; set; }

    public int? HfId { get; set; }

    public bool? IsActive { get; set; }

    public string? DhisFacilityCode { get; set; }
}
