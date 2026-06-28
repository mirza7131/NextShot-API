using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class AuditLog
{
    public long AuditLogId { get; set; }

    public string? PathAndQuery { get; set; }

    public string? RequestUrl { get; set; }

    public string? LocalPath { get; set; }

    public string? Method { get; set; }

    public int? Port { get; set; }

    public string? JsonBody { get; set; }

    public bool IsModelStateValid { get; set; }

    public string? ModelStateError { get; set; }

    public string? Token { get; set; }

    public string? Host { get; set; }

    public string? HostNameType { get; set; }

    public string? IdnHost { get; set; }

    public DateTime? RequestTime { get; set; }

    public DateTime? ResponseTime { get; set; }

    public string? ResponseData { get; set; }

    public bool IsError { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
