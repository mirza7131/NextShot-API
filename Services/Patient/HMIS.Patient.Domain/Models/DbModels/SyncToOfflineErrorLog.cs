using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class SyncToOfflineErrorLog
{
    public long SyncToOfflineErrorLogId { get; set; }

    public bool? Response { get; set; }

    public string? Json { get; set; }

    public string? ErrorNumber { get; set; }

    public string? ErrorSeverity { get; set; }

    public string? ErrorState { get; set; }

    public string? ErrorMethod { get; set; }

    public string? ErrorLine { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime? ReportedOn { get; set; }
}
