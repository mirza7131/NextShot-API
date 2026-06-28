using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class IndicatorHealthFacility
{
    public int IndicatorHfid { get; set; }

    public int Hfid { get; set; }

    public int IndicatorId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Indicator Indicator { get; set; } = null!;
}
