using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblPatientReferReceiveHistory
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public string? Cnic { get; set; }

    public string? InvestigationNurse { get; set; }

    public string? InvestigationMo { get; set; }

    public string? Diagnosis { get; set; }

    public string? Symptoms { get; set; }

    public string? Medicine { get; set; }

    public int? Created { get; set; }

    public int? CreatedBy { get; set; }

    public string Status { get; set; } = null!;
}
