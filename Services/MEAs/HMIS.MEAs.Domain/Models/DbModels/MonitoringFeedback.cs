using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class MonitoringFeedback
{
    public int Id { get; set; }

    public int? MonitoringMasterId { get; set; }

    public string? FacilityInchargeComment { get; set; }

    public string? Meacomment { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int SyncBy { get; set; }

    public DateTime SyncOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual ICollection<MonitoringAttachment> MonitoringAttachments { get; } = new List<MonitoringAttachment>();

    public virtual MonitoringMaster? MonitoringMaster { get; set; }
}
