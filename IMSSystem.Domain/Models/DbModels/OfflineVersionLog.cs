using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class OfflineVersionLog
{
    public Guid OfflineVersionLogId { get; set; }

    public int HealthFacilityId { get; set; }

    public Guid ProjectProfileId { get; set; }

    public string VersionNumber { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }

    public string? IpAddress { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual HealthFacility HealthFacility { get; set; } = null!;

    public virtual Profile ProjectProfile { get; set; } = null!;
}
