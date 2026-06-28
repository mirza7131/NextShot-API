using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class UserRole
{
    public Guid UserRoleId { get; set; }

    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
