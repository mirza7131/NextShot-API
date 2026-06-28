using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class DataSyncUtilityLog
{
    public Guid DataSyncUtilityLogId { get; set; }

    public int HealthFacilityId { get; set; }

    public string FileName { get; set; } = null!;

    public int Status { get; set; }

    public DateTime StatusUpdatedOn { get; set; }

    public DateTime? UploadedOn { get; set; }

    public DateTime? ProcessedOn { get; set; }

    public DateTime? CompletedOn { get; set; }

    public string? Message { get; set; }

    public string? ServerType { get; set; }

    public string? FileSize { get; set; }

    public int? FileStatus { get; set; }
}
