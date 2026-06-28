using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblBpNormalization
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public int? SampleId { get; set; }

    public int? Pulse { get; set; }

    public int? Systolic { get; set; }

    public int? Diastolic { get; set; }

    public int? Weight { get; set; }

    public string? BaselineType { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
