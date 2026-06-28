using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblDivision
{
    public int Id { get; set; }

    public string DivisionName { get; set; } = null!;

    public int DivisionCode { get; set; }

    public string Status { get; set; } = null!;
}
