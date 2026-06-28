using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ServiceProviderHealthFacility
{
    public Guid Id { get; set; }

    public Guid? SpId { get; set; }

    public int? HfId { get; set; }

    public bool? IsActive { get; set; }
}
