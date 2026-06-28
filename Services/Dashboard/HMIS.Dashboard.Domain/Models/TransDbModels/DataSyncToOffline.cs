using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class DataSyncToOffline
{
    public long DataSyncToOfflineId { get; set; }

    public string TableName { get; set; } = null!;

    public int HealthFacilityId { get; set; }

    public DateTime? SyncOn { get; set; }

    public DateTime? PrevSyncOn { get; set; }

    public byte ActionTypeId { get; set; }

    public DateTime ActionOn { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public string? WorkFlow { get; set; }

    public string? Json { get; set; }
}
