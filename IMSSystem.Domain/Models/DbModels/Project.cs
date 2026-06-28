using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Project
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? AuthKey { get; set; }

    public bool? IsActive { get; set; }
}
