using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblMissingUpdatedDatum
{
    public int Id { get; set; }

    public string? Mrn { get; set; }

    public string? B { get; set; }

    public string? C { get; set; }

    public int? Pid { get; set; }

    public int? UserHospital { get; set; }

    public int? Created { get; set; }
}
