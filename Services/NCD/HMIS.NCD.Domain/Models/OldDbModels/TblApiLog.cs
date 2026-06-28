using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblApiLog
{
    public int Id { get; set; }

    public string? ReqData { get; set; }

    public string? Module { get; set; }

    public int? Created { get; set; }

    public DateTime? LogDate { get; set; }

    public string? AppVersion { get; set; }
}
