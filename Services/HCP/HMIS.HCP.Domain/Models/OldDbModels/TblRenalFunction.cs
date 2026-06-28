using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblRenalFunction
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public int? SampleId { get; set; }

    public int? Urea { get; set; }

    public decimal? Creatinie { get; set; }

    public int? BloodSugarRandom { get; set; }

    public string? BaselineType { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
