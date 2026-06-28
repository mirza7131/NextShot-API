using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class SmsForMep
{
    public string? BothNegativeMessage { get; set; }

    public string? PcrPositiveMessage { get; set; }

    public string? SvrMessage { get; set; }
}
