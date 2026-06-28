using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblLabSample
{
    public int Id { get; set; }

    public string? SampleNumber { get; set; }

    public string? BatchNumber { get; set; }

    public string? TestType { get; set; }

    public int? ActionId { get; set; }

    public int? Pid { get; set; }

    public int? HwPid { get; set; }

    public int? MicroId { get; set; }

    public int? UserId { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public string IsLabBatch { get; set; } = null!;

    public string IsResample { get; set; } = null!;

    public string IsRefered { get; set; } = null!;

    public int? RejectedBy { get; set; }

    public int? RejectedTime { get; set; }

    public string? IsSampleSvr { get; set; }

    public int? TotalDuplicate { get; set; }

    public string IsTerminate { get; set; } = null!;

    public string IsIgnore { get; set; } = null!;

    public int? HospitalId { get; set; }
}
