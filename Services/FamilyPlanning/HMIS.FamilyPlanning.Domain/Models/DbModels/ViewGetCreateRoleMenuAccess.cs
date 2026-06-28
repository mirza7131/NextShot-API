using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class ViewGetCreateRoleMenuAccess
{
    public Guid? RoleMenuId { get; set; }

    public Guid? RoleId { get; set; }

    public Guid MenuId { get; set; }

    public bool? IsModule { get; set; }

    public Guid? ModuleId { get; set; }

    public string? Name { get; set; }

    public string? DisplayName { get; set; }

    public int? SeqNo { get; set; }

    public Guid? ParentId { get; set; }

    public bool? HasAccess { get; set; }

    public bool? CanWrite { get; set; }

    public bool? CanRead { get; set; }

    public bool? CanEdit { get; set; }

    public bool? CanDelete { get; set; }
}
