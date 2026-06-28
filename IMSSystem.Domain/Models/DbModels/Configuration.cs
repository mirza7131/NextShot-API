using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Configuration
{
    public int Id { get; set; }

    public DateTime? Deadline { get; set; }

    public bool? IsActive { get; set; }
}
