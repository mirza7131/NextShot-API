using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblHealthFacilityType
{
    public int Id { get; set; }

    public int HfTypeCode { get; set; }

    public string HealthFacilityName { get; set; } = null!;

    public int Sorting { get; set; }
}
