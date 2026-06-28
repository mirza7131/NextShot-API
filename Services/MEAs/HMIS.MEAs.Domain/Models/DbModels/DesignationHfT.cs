using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class DesignationHfT
{
    public int DesignationHfTypeId { get; set; }

    public int? HfTypeId { get; set; }

    public int? DesignationId { get; set; }

    public int? ShiftId { get; set; }

    public virtual Designation? Designation { get; set; }

    public virtual HealthFacilityType? HfType { get; set; }
}
