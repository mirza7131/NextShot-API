using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblLostFollowupReason
{
    public int Id { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime? Created { get; set; }
}
