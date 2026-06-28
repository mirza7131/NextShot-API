using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class TblIndicatorMap
{
    public int IndicatorId { get; set; }

    public string? Question { get; set; }

    public string? ShortQuestion { get; set; }
}
