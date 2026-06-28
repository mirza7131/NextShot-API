using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class UserPermission
{
    public int Id { get; set; }

    public string? RoleId { get; set; }

    public string? UserId { get; set; }

    public int? PermissionId { get; set; }

    public string? Description { get; set; }

    public string? Remarks { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? CreatedByUserId { get; set; }

    public bool? IsActive { get; set; }
}
