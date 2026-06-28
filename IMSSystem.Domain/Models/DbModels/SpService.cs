using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class SpService
{
    public Guid Id { get; set; }

    public Guid? SpId { get; set; }

    public Guid? ServiceTypeId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ServiceProvider? Sp { get; set; }
}
