using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class ViewLocation
{
    public int ProvinceId { get; set; }

    public string? ProvinceCode { get; set; }

    public string? ProvinceName { get; set; }

    public int DivisionId { get; set; }

    public string? DivisionCode { get; set; }

    public string? DivisionName { get; set; }

    public int DistrictId { get; set; }

    public string? DistrictCode { get; set; }

    public string? DistrictName { get; set; }

    public int TehsilId { get; set; }

    public string? TehsilCode { get; set; }

    public string? TehsilName { get; set; }

    public int HealthFacilityId { get; set; }

    public string? HealthFacilityCode { get; set; }

    public string? HealthFacilityName { get; set; }
}
