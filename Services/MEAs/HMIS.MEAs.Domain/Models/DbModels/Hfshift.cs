using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Hfshift
{
    public int HfshiftId { get; set; }

    public int? HftypeId { get; set; }

    public int? ShiftId { get; set; }

    public virtual HealthFacilityType? Hftype { get; set; }

    public virtual Shift? Shift { get; set; }
}
