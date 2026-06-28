using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class ErrorLog
{
    public long ErrorLogId { get; set; }

    public string? Message { get; set; }

    public string? StackTrace { get; set; }

    public string? InnerException { get; set; }

    public string? Method { get; set; }

    public string? Route { get; set; }

    public string? RouteBase { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
