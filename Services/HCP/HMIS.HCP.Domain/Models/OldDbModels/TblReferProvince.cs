using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblReferProvince
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public string? Status { get; set; }
}
