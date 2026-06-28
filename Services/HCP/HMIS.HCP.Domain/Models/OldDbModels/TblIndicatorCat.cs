using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblIndicatorCat
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Status { get; set; }
}
