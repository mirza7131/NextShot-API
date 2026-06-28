using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class ModuleFlood
{
    public int ModuleId { get; set; }

    public string? ModuleName { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsRequired { get; set; }
}
