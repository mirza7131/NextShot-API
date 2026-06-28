using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class HealthFacilityStation
{
    public Guid HealthFacilityStationId { get; set; }

    public Guid? StationProfileId { get; set; }

    public short? SequenceNo { get; set; }

    public int? HealthFacilityId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual HealthFacility? HealthFacility { get; set; }

    public virtual Profile? StationProfile { get; set; }
}
