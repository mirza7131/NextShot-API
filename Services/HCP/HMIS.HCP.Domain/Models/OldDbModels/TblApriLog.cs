using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblApriLog
{
    public int Id { get; set; }

    public string? ApriValue { get; set; }

    public string? Comments { get; set; }

    public string? TestType { get; set; }

    public DateTime Created { get; set; }
}
