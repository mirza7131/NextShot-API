using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class ClientFollowup
{
    public Guid FollowupClientId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? MethodInUseProfileId { get; set; }

    public string SatisfiedWithCurrentMethod { get; set; } = null!;

    public string? Reason { get; set; }

    public string ContinueWithTheSame { get; set; } = null!;

    public int Quantity { get; set; }

    public string? RemovalOfMethod { get; set; }

    public DateTime? RemovalMethodStartDate { get; set; }

    public DateTime? RemovalMethodEndDate { get; set; }

    public Guid? ReasonForRemovalProfileId { get; set; }

    public string? OtherReasonForRemoval { get; set; }

    public Guid? SwitchedMethodProfileId { get; set; }

    public int? SwitchedMethodQuantity { get; set; }

    public DateTime FollowUpVisitDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public bool IsActive { get; set; }

    public byte ActionTypeId { get; set; }
}
