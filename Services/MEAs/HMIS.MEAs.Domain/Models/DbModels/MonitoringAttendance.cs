using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class MonitoringAttendance
{
    public int Id { get; set; }

    public int? MonitoringMasterId { get; set; }

    public string? Name { get; set; }

    public string? Cnic { get; set; }

    public int? DesignationId { get; set; }

    public string? ContactNo { get; set; }

    public int? PresenceStatusId { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int SyncBy { get; set; }

    public DateTime SyncOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual MonitoringMaster? MonitoringMaster { get; set; }
}
