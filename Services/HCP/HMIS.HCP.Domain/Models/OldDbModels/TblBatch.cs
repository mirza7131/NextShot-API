using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblBatch
{
    public long Id { get; set; }

    public string? BactchNumber { get; set; }

    public string IsBatchDispatched { get; set; } = null!;

    public string IsBatchReceived { get; set; } = null!;

    public int? UserHospital { get; set; }

    public int? UserId { get; set; }

    public int? Created { get; set; }
}
