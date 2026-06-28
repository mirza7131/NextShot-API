using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblDispatch
{
    public long Id { get; set; }

    public string? Batchno { get; set; }

    public string? Courier { get; set; }

    public string? Consignment { get; set; }

    public string? Name { get; set; }

    public string? Cnic { get; set; }

    public string? Contactno { get; set; }

    public string? Designation { get; set; }

    public string? Organization { get; set; }

    public int? Created { get; set; }

    public int? UserId { get; set; }

    public string Dispatchmade { get; set; } = null!;
}
