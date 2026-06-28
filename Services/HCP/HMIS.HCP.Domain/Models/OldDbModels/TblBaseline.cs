using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblBaseline
{
    public int Id { get; set; }

    public double? Hemoglobin { get; set; }

    public double? Ast { get; set; }

    public double? Alt { get; set; }

    public double? Platelet { get; set; }

    public double? Tlc { get; set; }

    public double? Apri { get; set; }

    public string? HcvPcr { get; set; }

    public string? HbvPcr { get; set; }

    public double? HcvViralLoad { get; set; }

    public double? HbvViralLoad { get; set; }

    public string? Liver { get; set; }

    public string? Spleen { get; set; }

    public string? Ascites { get; set; }

    public string? Genotyping { get; set; }

    public string? IsSkip { get; set; }

    public string? PrescribeMedicine { get; set; }

    public string? IsHbvMedicine { get; set; }

    public string? HbvMedicine { get; set; }

    public string? IsHcvMedicine { get; set; }

    public string? HcvMedicine { get; set; }

    public string? Counselling { get; set; }

    public string? Vaccination { get; set; }

    public string? VaccinationOption { get; set; }

    public string? Discharge { get; set; }

    public int? PatientId { get; set; }

    public int? Created { get; set; }

    public string? Deferred { get; set; }

    public string? ReferralPkli { get; set; }

    public int? UserId { get; set; }

    public string IsDemote { get; set; } = null!;

    public int? FollowUpNumber { get; set; }

    public int? UserHospital { get; set; }

    public int? Updated { get; set; }

    public int? UpdatedBy { get; set; }
}
