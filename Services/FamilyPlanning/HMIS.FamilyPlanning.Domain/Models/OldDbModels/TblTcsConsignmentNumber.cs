using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblTcsConsignmentNumber
{
    public int Id { get; set; }

    public long MinRange { get; set; }

    public long MaxRange { get; set; }

    public string IsActive { get; set; } = null!;

    public int? Created { get; set; }
}
