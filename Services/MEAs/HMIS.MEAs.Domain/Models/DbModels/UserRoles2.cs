using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class UserRoles2
{
    public int UserRoleId { get; set; }

    public int? RoleId { get; set; }

    public int? UserId { get; set; }
}
