using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class Prosthetic
{
    public Guid ProstheticId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientOpenVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? CauseProfileId { get; set; }

    public string? OtherCause { get; set; }

    public int? NumberOfDevicesDelivered { get; set; }

    public string? OtherPurposeOfVisit { get; set; }

    public string? OtherPreOrthoticProstheticTreatment { get; set; }

    public int? NumberOfOrthoticProstheticUsedBefore { get; set; }

    public string? PastHistoryOrCommodities { get; set; }

    public string? ObservationalGaitAnalysis { get; set; }

    public string? TreatmentObjective { get; set; }

    public string? Notes { get; set; }

    public string? PurposeOfFollowup { get; set; }

    public bool IsActive { get; set; }

    public int ActionTypeId { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }
}
