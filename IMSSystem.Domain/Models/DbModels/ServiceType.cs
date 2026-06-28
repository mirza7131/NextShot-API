using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ServiceType
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public string? DisplayName { get; set; }

    public bool? IsActive { get; set; }
}
