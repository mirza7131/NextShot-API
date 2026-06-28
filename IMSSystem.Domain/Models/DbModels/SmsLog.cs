using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class SmsLog
{
    public int Id { get; set; }

    public int? SmsSessionId { get; set; }

    public string? MessageId { get; set; }

    public string? Number { get; set; }

    public string? Message { get; set; }

    public int? Fkid { get; set; }

    public string? Status { get; set; }

    public string? Otp { get; set; }

    public string? Mask { get; set; }

    public string? UserId { get; set; }

    public DateTime? DateTime { get; set; }

    public string? SmsrecieverUserId { get; set; }
}
