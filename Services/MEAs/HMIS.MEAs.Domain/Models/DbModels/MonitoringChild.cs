using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class MonitoringChild
{
    public int Id { get; set; }

    public int? MonitoringMasterId { get; set; }

    public int? IndicatorId { get; set; }

    public string? Answer { get; set; }

    public string? Remarks { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int SyncBy { get; set; }

    public DateTime SyncOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? ModuleId { get; set; }

    public virtual Indicator? Indicator { get; set; }

    public virtual MonitoringMaster? MonitoringMaster { get; set; }
}
