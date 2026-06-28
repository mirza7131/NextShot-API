using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class EmrUserToken
{
    public Guid UserTokenId { get; set; }

    public string UserId { get; set; } = null!;

    public string DeviceId { get; set; } = null!;

    public string Token { get; set; } = null!;

    public bool IsValid { get; set; }

    public DateTime TokenExpireDatetime { get; set; }

    public DateTime? DateTimeCreatedAt { get; set; }

    public DateTime? DateTimeUpdatedAt { get; set; }

    public string? UserIdCreatedBy { get; set; }

    public string? UserIdUpdatedBy { get; set; }
}
