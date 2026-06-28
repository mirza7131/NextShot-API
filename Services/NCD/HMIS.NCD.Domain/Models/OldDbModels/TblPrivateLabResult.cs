using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblPrivateLabResult
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public string? TestRequired { get; set; }

    public string? IsHbvDetected { get; set; }

    public string? Hbv { get; set; }

    public string? IsHcvDetected { get; set; }

    public string? Hcv { get; set; }

    public int? Created { get; set; }

    public int? UserId { get; set; }

    public string? LabName { get; set; }

    public string? ResultType { get; set; }
}
