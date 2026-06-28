using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblApiConfig
{
    public int Id { get; set; }

    public string? FromDate { get; set; }

    public string? ToDate { get; set; }

    public string? Created { get; set; }
}
