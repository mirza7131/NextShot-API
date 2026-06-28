using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class ApplicationModule
{
    public int ApplicationModuleId { get; set; }

    public int? ModuleId { get; set; }

    public int? ApplicationTypeId { get; set; }

    public virtual ApplicationType? ApplicationType { get; set; }

    public virtual Module? Module { get; set; }
}
