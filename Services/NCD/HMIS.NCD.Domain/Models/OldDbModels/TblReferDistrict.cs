using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblReferDistrict
{
    public int Id { get; set; }

    public string? DistrictName { get; set; }

    public string? ProvinceId { get; set; }

    public int DistrictCode { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public string Status { get; set; } = null!;
}
