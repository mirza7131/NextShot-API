using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class CategoryShift
{
    public int CategoryShiftId { get; set; }

    public int? ShiftId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Shift? Shift { get; set; }
}
