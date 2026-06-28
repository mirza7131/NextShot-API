using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class MedicalHistory
{
    public Guid MedicalHistoryId { get; set; }

    public Guid RegistrationDetailId { get; set; }

    public bool IsDiabetic { get; set; }

    public bool IsHypertension { get; set; }

    public bool IsMigraine { get; set; }

    public bool IsSmoking { get; set; }

    public bool IsBreastfeeding { get; set; }

    public bool IsLastDelivery { get; set; }

    public bool IsMiscarriages { get; set; }

    public bool IsAbortion { get; set; }

    public string? PelvicInflamatoryDisease { get; set; }

    public string? Investigations { get; set; }

    public string? OtherInvestigations { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
