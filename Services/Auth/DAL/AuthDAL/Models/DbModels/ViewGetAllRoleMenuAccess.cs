using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class ViewGetAllRoleMenuAccess
{
    public Guid MenuId { get; set; }

    public Guid? ModuleId { get; set; }

    public string? Name { get; set; }

    public string? DisplayName { get; set; }

    public string? Url { get; set; }

    public string? Icon { get; set; }

    public int? SeqNo { get; set; }

    public Guid? ParentId { get; set; }

    public bool? IsApi { get; set; }

    public bool? IsLabel { get; set; }

    public bool? IsModule { get; set; }

    public bool? IsDisplayMenu { get; set; }

    public bool? IsActive { get; set; }

    public string? ImageUrl { get; set; }

    public Guid UserId { get; set; }

    public bool CanDelete { get; set; }

    public bool CanEdit { get; set; }

    public bool CanRead { get; set; }

    public bool CanWrite { get; set; }
}
