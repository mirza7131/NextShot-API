using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblTcsBatch
{
    public int Id { get; set; }

    public string? BatchType { get; set; }

    public string? BatchNumber { get; set; }

    public int? UserId { get; set; }

    public string? HfcAddress { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }
}
