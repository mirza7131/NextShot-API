using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblPatientTransfer
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? TransferInFacility { get; set; }

    public string? TransferInFacilityName { get; set; }

    public int? TransferOutFacility { get; set; }

    public string? TransferRequestDate { get; set; }

    public string? ActionDate { get; set; }

    public string TransferStatus { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public int? Created { get; set; }

    public string? PatientStage { get; set; }

    public string? PatientType { get; set; }

    public string? Vaccination { get; set; }

    public int? VaccinationDosesCount { get; set; }

    public string? ScreeningHbv { get; set; }

    public string? ScreeningHcv { get; set; }

    public int? HbvMedDelivered { get; set; }

    public int? HcvMedDelivered { get; set; }

    public int? HbvFollowups { get; set; }

    public int? HcvFollowups { get; set; }

    public string? HbvMedicineName { get; set; }

    public string? HcvMedicineName { get; set; }

    public string? TransferReason { get; set; }
}
