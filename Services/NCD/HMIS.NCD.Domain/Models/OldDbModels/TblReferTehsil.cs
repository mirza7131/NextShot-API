using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblReferTehsil
{
    public int Id { get; set; }

    public int? DistrictId { get; set; }

    public string? Name { get; set; }

    public string TeshilCode { get; set; } = null!;

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public string Status { get; set; } = null!;
}
