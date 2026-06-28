using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class UserLocation
{
    public int UserLocationId { get; set; }

    public int UserId { get; set; }

    public string? LocationCode { get; set; }

    public virtual User User { get; set; } = null!;
}
