using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class OutSourceSystemLog
{
    public Guid OutSourceSystemLogId { get; set; }

    public Guid? SourceSystemId { get; set; }

    public string? UserSystemIp { get; set; }

    public string? TargetedApiUrl { get; set; }

    public Guid? CreatedBy { get; set; }

    public string? InputParameters { get; set; }

    public DateTime? CreatedOn { get; set; }
}
