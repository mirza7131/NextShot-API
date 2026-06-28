using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class OfflineServerDataSyncLog
{
    public Guid OfflineServerDataSyncLogId { get; set; }

    public DateTime? LastSync { get; set; }

    public int? HealthFacilityId { get; set; }
}
