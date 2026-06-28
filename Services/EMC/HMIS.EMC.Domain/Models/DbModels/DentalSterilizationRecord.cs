using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class DentalSterilizationRecord
{
    public Guid DentalSterilizationRecordId { get; set; }

    public Guid? EquipmentProfileId { get; set; }

    public int? NoOfPouches { get; set; }

    public bool? CloseToExpiryDentalMaterial { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }

    public int? HealthFacilityId { get; set; }

    public bool? IsActive { get; set; }

    public string? Remarks { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Profile? EquipmentProfile { get; set; }
}
