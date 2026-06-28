using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class ApplicationType
{
    public int ApplicationTypeId { get; set; }

    public string? ApplicationTypeName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ApplicationHftype> ApplicationHftypes { get; } = new List<ApplicationHftype>();

    public virtual ICollection<ApplicationModule> ApplicationModules { get; } = new List<ApplicationModule>();

    public virtual ICollection<Category> Categories { get; } = new List<Category>();

    public virtual ICollection<Zone> Zones { get; } = new List<Zone>();
}
