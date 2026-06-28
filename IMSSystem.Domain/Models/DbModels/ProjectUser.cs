using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ProjectUser
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public int? ProjectId { get; set; }

    public bool? IsActive { get; set; }
}
