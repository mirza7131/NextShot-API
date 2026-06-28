using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblCronJobsLog
{
    public int Id { get; set; }

    public string? CronType { get; set; }

    public int? Created { get; set; }

    public DateTime? ExecutionStartTime { get; set; }

    public DateTime? ExecutionEndTime { get; set; }
}
