using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class SmsSession
{
    public int Id { get; set; }

    public string? SessionId { get; set; }

    public DateTime? LastActivityTime { get; set; }

    public DateTime? RequestDateTime { get; set; }

    public string? RequestedBy { get; set; }
}
