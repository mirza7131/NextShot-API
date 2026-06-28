using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Module
{
    public int ModuleId { get; set; }

    public string? ModuleName { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsRequired { get; set; }

    public virtual ICollection<ApplicationModule> ApplicationModules { get; } = new List<ApplicationModule>();

    public virtual ICollection<Category> Categories { get; } = new List<Category>();
}
