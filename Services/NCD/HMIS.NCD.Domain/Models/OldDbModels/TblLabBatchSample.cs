using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblLabBatchSample
{
    public int? SampleId { get; set; }

    public int? BatchId { get; set; }

    public string IsDiscard { get; set; } = null!;
}
