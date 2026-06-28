using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class OptionType
{
    public int OptionTypeId { get; set; }

    public string? OptionTypeName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<IndicatorOption> IndicatorOptions { get; } = new List<IndicatorOption>();

    public virtual ICollection<Indicator> Indicators { get; } = new List<Indicator>();

    public virtual ICollection<IndicatorsRecov> IndicatorsRecovs { get; } = new List<IndicatorsRecov>();
}
