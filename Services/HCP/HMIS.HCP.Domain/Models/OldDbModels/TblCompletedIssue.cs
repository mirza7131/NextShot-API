using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblCompletedIssue
{
    public int Id { get; set; }

    public string? MrnNo { get; set; }

    public string Hbv { get; set; } = null!;

    public string Hcv { get; set; } = null!;
}
