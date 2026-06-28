using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class ViewLocationMea
{
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

    public int? ZoneId { get; set; }

    public int? HfId { get; set; }

    public int Active { get; set; }

    public string? AmbulanceNo { get; set; }

    public string? DhisFacilityCode { get; set; }

    public string? Phase { get; set; }

    public bool? IntegratedRhc { get; set; }
}
