using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblLabResult
{
    public int Id { get; set; }

    public int? SampleId { get; set; }

    public string? TestRequired { get; set; }

    public string? IsHbvDetected { get; set; }

    public string? Hbv { get; set; }

    public string? IsHcvDetected { get; set; }

    public string? Hcv { get; set; }

    public int? Created { get; set; }

    public int? UserId { get; set; }

    public int? BatchId { get; set; }

    public string IsBatchDiscard { get; set; } = null!;

    public string IsSoftDelete { get; set; } = null!;
}
