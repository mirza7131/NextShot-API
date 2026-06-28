using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class IndicatorShift
{
    public int IndicatorShiftId { get; set; }

    public int? IndicatorId { get; set; }

    public int? ShiftId { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Indicator? Indicator { get; set; }

    public virtual Shift? Shift { get; set; }
}
