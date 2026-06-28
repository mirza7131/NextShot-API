using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class ExceptionLog
{
    public int Id { get; set; }

    public int? FkHcpid { get; set; }

    public string? ExMessage { get; set; }
}
