using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblPatientStageLog
{
    public long Id { get; set; }

    public int? Pid { get; set; }

    public int? StageId { get; set; }

    public int? Created { get; set; }
}
