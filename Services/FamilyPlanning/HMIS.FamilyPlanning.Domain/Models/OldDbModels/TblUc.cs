using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblUc
{
    public int Id { get; set; }

    public string? UcName { get; set; }

    public int? TehsilId { get; set; }

    public int? TehsilCode { get; set; }

    public string IsActive { get; set; } = null!;
}
