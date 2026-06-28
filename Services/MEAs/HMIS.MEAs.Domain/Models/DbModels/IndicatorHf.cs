using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class IndicatorHf
{
    public int InidcatorHfTypeId { get; set; }

    public int? IndicatorId { get; set; }

    public int? HfTypeId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual HealthFacilityType? HfType { get; set; }

    public virtual Indicator? Indicator { get; set; }
}
