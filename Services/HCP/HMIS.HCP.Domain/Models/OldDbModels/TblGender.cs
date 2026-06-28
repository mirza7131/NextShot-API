using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblGender
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Status { get; set; }
}
