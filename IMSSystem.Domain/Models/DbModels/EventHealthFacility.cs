using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class EventHealthFacility
{
    public Guid EventHealthFacilityId { get; set; }

    public int HealthFacilityId { get; set; }

    public Guid EventId { get; set; }

    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public bool IsActive { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }
}
