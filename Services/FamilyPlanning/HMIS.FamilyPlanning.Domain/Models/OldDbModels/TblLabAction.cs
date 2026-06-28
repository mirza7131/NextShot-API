using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblLabAction
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Status { get; set; } = null!;
}
