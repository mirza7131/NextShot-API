using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class SpEmployee
{
    public Guid? Id { get; set; }

    public Guid? SpId { get; set; }

    public Guid? ServiceTypeId { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
