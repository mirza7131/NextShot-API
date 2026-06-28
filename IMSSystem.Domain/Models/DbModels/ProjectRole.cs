using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ProjectRole
{
    public int Id { get; set; }

    public string? RoleId { get; set; }

    public int? ProjectId { get; set; }

    public bool? IsActive { get; set; }
}
