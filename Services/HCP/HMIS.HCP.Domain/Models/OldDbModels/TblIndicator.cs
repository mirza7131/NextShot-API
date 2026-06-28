using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblIndicator
{
    public string Id { get; set; } = null!;

    public string? Category { get; set; }

    public string? Name { get; set; }

    public string Status { get; set; } = null!;

    public int SortOrder { get; set; }

    public string? WeeklyReport { get; set; }
}
