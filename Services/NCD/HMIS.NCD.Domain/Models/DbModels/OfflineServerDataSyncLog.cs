using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class OfflineServerDataSyncLog
{
    public Guid OfflineServerDataSyncLogId { get; set; }

    public DateTime? LastSync { get; set; }

    public int? HealthFacilityId { get; set; }
}
