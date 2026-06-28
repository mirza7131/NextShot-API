using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblDistrict
{
    public int Id { get; set; }

    public string DistrictName { get; set; } = null!;

    public int DivisionCode { get; set; }

    public int DistrictCode { get; set; }

    public string? IsActive { get; set; }
}
