using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Ward
{
    public int Id { get; set; }

    public string? WardName { get; set; }

    public bool? IsActive { get; set; }
}
