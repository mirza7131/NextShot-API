using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Region
{
    public int RegionId { get; set; }

    public string RegionName { get; set; } = null!;

    public bool? IsActive { get; set; }
}
