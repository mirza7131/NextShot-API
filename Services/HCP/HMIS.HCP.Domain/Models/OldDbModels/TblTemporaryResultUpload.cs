using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblTemporaryResultUpload
{
    public int Id { get; set; }

    public string? SampleNo { get; set; }

    public int? SampleId { get; set; }

    public string? TestRequired { get; set; }

    public string? ResultHbv { get; set; }

    public string? HbvViralLoad { get; set; }

    public string? ResultHcv { get; set; }

    public string? HcvViralLoad { get; set; }

    public string? IsFound { get; set; }

    public int? Created { get; set; }

    public string? IsBatchCountMatched { get; set; }

    public int? BatchId { get; set; }

    public int? Pid { get; set; }

    public string? IsSvrSample { get; set; }
}
