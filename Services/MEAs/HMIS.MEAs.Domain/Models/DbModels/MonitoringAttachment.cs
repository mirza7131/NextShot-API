using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class MonitoringAttachment
{
    public int Id { get; set; }

    public int? FeedbackId { get; set; }

    public string? ImagePath { get; set; }

    public string? ImageName { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int SyncBy { get; set; }

    public DateTime SyncOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual MonitoringFeedback? Feedback { get; set; }
}
