using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblRole
{
    public int Id { get; set; }

    public string? Roles { get; set; }

    public string? Status { get; set; }

    public int? Updated { get; set; }

    public int? Created { get; set; }
}
