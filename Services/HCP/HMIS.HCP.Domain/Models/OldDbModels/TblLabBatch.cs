using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblLabBatch
{
    public int Id { get; set; }

    public string? BatchNumber { get; set; }

    public int? UserId { get; set; }

    public int? AssignedTo { get; set; }

    public int? AssignedBy { get; set; }

    public int? CreatedTime { get; set; }

    public int? UpdateTime { get; set; }

    public string IsResult { get; set; } = null!;

    public int? ResultTime { get; set; }
}
