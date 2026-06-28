using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class InputType
{
    public int InputTypeId { get; set; }

    public string? InputTypeName { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<IndicatorOption> IndicatorOptions { get; } = new List<IndicatorOption>();

    public virtual ICollection<Indicator> Indicators { get; } = new List<Indicator>();

    public virtual ICollection<IndicatorsRecov> IndicatorsRecovs { get; } = new List<IndicatorsRecov>();
}
