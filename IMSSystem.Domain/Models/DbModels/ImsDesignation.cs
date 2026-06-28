using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ImsDesignation
{
    public Guid? Id { get; set; }

    public Guid? StakeHolderTypeId { get; set; }

    public Guid? StakeHolderId { get; set; }

    public Guid? ServiceTypeId { get; set; }

    public string? ShortName { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }
}
