using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class UserToken
{
    public Guid UserTokenId { get; set; }

    public Guid UserId { get; set; }

    public string? DeviceId { get; set; }

    public string Token { get; set; } = null!;

    public bool IsValid { get; set; }

    public DateTime TokenExpireDatetime { get; set; }

    public DateTime? DateTimeCreatedAt { get; set; }

    public DateTime? DateTimeUpdatedAt { get; set; }

    public Guid? UserIdCreatedBy { get; set; }

    public Guid? UserIdUpdatedBy { get; set; }

    public DateTime? DateTimeDeletedAt { get; set; }

    public Guid? UserIdDeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
