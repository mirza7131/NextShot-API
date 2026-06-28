using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class FileUploadedToMasterServerLog
{
    public Guid FileUploadedToMasterServerLogId { get; set; }

    public string? FileName { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? UploadedDateTime { get; set; }

    public bool? IsProcessed { get; set; }

    public DateTime? ProcessOn { get; set; }

    public string? CdnUrl { get; set; }
}
