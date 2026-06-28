using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class OfflineServerDataSyncLog
{
    public Guid OfflineServerDataSyncLogId { get; set; }

    public DateTime? LastSync { get; set; }

    public int? HealthFacilityId { get; set; }
}
