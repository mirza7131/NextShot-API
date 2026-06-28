using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class TbmappedHealthFacility
{
    public string? DivisionCode { get; set; }

    public string DivisionName { get; set; } = null!;

    public string? DistrictCode { get; set; }

    public string DistrictName { get; set; } = null!;

    public string? TehsilCode { get; set; }

    public string TehsilName { get; set; } = null!;

    public string? CenterId { get; set; }

    public string FullName { get; set; } = null!;
}
