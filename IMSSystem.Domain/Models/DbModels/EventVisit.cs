using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class EventVisit
{
    public Guid EventVisitId { get; set; }

    public Guid PatientVisitId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid EventId { get; set; }

    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }
}
