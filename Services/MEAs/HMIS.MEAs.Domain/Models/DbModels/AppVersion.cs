using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class AppVersion
{
    public int AppVersionId { get; set; }

    public string? Version { get; set; }

    public int? ChecklistPrimary { get; set; }

    public int? ChecklistSceondary { get; set; }

    public int? ChecklistMacs { get; set; }
}
