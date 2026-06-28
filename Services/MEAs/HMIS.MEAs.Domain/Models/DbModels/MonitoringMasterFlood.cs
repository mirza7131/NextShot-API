using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class MonitoringMasterFlood
{
    public int Id { get; set; }

    public int? ModuleId { get; set; }

    public int? CampId { get; set; }

    public string? DistrictCode { get; set; }

    public string? TehsilCode { get; set; }

    public string? CloseReason { get; set; }

    public string? Comments { get; set; }

    public bool? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? SyncBy { get; set; }

    public DateTime? SyncOn { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? InchargeName { get; set; }

    public int? InchargeDesignation { get; set; }

    public string? InchargeMobileNo { get; set; }

    public string? InchargeCnic { get; set; }
}
