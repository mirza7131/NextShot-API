using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblBloodBankResult
{
    public int Id { get; set; }

    public int Pid { get; set; }

    public string? HcvScreeningResult { get; set; }

    public string? HbvScreeningResult { get; set; }

    public string? ScreeningMethods { get; set; }

    public int? Created { get; set; }

    public int? CreatedBy { get; set; }

    public string? PatientType { get; set; }
}
