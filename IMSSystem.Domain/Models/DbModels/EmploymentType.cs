using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class EmploymentType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }
}
