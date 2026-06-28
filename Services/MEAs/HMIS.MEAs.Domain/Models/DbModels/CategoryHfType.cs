using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class CategoryHfType
{
    public int CategoryHftypeId { get; set; }

    public int? CategoryId { get; set; }

    public int? HfTypeId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual HealthFacilityType? HfType { get; set; }
}
