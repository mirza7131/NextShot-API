using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class GeolvlV
{
    public string Pkcode { get; set; } = null!;

    public string? Fkcode { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string Lvl { get; set; } = null!;

    public long? Lnth { get; set; }
}
