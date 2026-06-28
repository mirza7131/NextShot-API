using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class StakeHolderType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Guid? ParentStakeHolderId { get; set; }

    public string? DisplayName { get; set; }

    public bool? IsActive { get; set; }
}
