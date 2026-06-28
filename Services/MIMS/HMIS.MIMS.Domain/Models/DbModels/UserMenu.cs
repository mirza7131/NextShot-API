using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class UserMenu
{
    public Guid UserPermissionId { get; set; }

    public Guid UserId { get; set; }

    public Guid MenuId { get; set; }

    public DateTime? DateTimeCreatedAt { get; set; }

    public Guid? UserIdCreatedBy { get; set; }

    public DateTime? DateTimeUpdatedAt { get; set; }

    public Guid? UserIdUpdatedBy { get; set; }

    public DateTime? DateTimeDeletedAt { get; set; }

    public Guid? UserIdDeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
