using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblCallcenter
{
    public int Id { get; set; }

    public string? MrnNo { get; set; }

    public int? Age { get; set; }

    public string? Name { get; set; }

    public string? Fathername { get; set; }

    public int? Gender { get; set; }

    public int? CnicStatus { get; set; }

    public string? SelfCnic { get; set; }

    public string? NextOfKinCnic { get; set; }

    public string? NoCnicReason { get; set; }

    public int? Division { get; set; }

    public int? District { get; set; }

    public int? Tehsil { get; set; }

    public int? Hospital { get; set; }

    public string? Address { get; set; }

    public string? LandMark { get; set; }

    public string ContactPhone { get; set; } = null!;

    public string? AltPhone { get; set; }

    public int? NoOfDependents { get; set; }

    public string? MonthlyIncome { get; set; }

    public int? Hbv { get; set; }

    public int? Hcv { get; set; }

    public string? LabName { get; set; }

    public DateTime? TestDate { get; set; }

    public int UserId { get; set; }

    public int? Created { get; set; }

    public string? CreatedB { get; set; }

    public string IsPatient { get; set; } = null!;
}
