using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class HwdPatientSearchLog
{
    public int Id { get; set; }

    public string? Cnic { get; set; }

    public int? UserId { get; set; }

    public int? Created { get; set; }
}
