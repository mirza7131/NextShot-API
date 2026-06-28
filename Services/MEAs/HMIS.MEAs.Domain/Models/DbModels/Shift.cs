using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Shift
{
    public int ShiftId { get; set; }

    public string? ShiftName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<CategoryShift> CategoryShifts { get; } = new List<CategoryShift>();

    public virtual ICollection<Hfshift> Hfshifts { get; } = new List<Hfshift>();

    public virtual ICollection<IndicatorShift> IndicatorShifts { get; } = new List<IndicatorShift>();
}
