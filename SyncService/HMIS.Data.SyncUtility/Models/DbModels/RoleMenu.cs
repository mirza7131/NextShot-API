using System;
using System.Collections.Generic;

namespace HMIS.Data.SyncUtility.Models.DbModels;

public partial class RoleMenu
{
    public Guid RoleMenuId { get; set; }

    public Guid MenuId { get; set; }

    public Guid RoleId { get; set; }

    public bool CanRead { get; set; }

    public bool CanWrite { get; set; }

    public bool CanEdit { get; set; }

    public bool CanDelete { get; set; }

    public bool? HasAccess { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
