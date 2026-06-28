using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblOccupation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Created { get; set; }

    public string? Status { get; set; }
}
