using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class Role
{
    public Guid RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }

    public bool IsShowHfadmin { get; set; }

    public bool IsActive { get; set; }

    public DateTime? SyncDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public string? RoutingUrl { get; set; }

    public virtual ICollection<RoleMenu> RoleMenus { get; } = new List<RoleMenu>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
