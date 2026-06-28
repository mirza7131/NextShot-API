using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblBatchSample
{
    public long Id { get; set; }

    public int? SampleNumber { get; set; }

    public int? BatchId { get; set; }

    public int? UserId { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }
}
