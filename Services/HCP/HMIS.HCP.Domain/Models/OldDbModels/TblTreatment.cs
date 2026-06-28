using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblTreatment
{
    public int Id { get; set; }

    public string RapidTesting { get; set; } = null!;

    public string? IsHbvTest { get; set; }

    public string? IsHcvTest { get; set; }

    public string PrescribeMedicine { get; set; } = null!;

    public string? IsHbvMedicine { get; set; }

    public string? IsHcvMedicine { get; set; }

    public string Counselling { get; set; } = null!;

    public string Vaccination { get; set; } = null!;

    public string? VaccinationOption { get; set; }

    public string? Pcr { get; set; }

    public string Discharge { get; set; } = null!;

    public string Deferred { get; set; } = null!;

    public string ReferralPkli { get; set; } = null!;

    public int PatientId { get; set; }

    public int Created { get; set; }

    public int UserId { get; set; }

    public string? HcvMedicine { get; set; }

    public string? HbvMedicine { get; set; }
}
