using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblPatientRefer
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? ReferInFacility { get; set; }

    public int? ReferInTehsil { get; set; }

    public int? ReferInDistrict { get; set; }

    public int? ReferInProvince { get; set; }

    public int? ReferOutFacility { get; set; }

    public string? ReferRequestDate { get; set; }

    public string? ActionDate { get; set; }

    public string ReferStatus { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public int? Created { get; set; }

    public string? PatientStage { get; set; }

    public string? PatientType { get; set; }

    public string? ReferReason { get; set; }
}
