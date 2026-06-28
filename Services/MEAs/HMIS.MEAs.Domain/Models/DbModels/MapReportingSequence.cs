using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class MapReportingSequence
{
    public int Id { get; set; }

    public int? IndicatorId { get; set; }

    public int? Sequence { get; set; }

    public string? Question { get; set; }

    public string? ShortQuestion { get; set; }
}
