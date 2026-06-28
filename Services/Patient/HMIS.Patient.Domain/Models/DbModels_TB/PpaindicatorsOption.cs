using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PpaindicatorsOption
{
    public int Id { get; set; }

    public int? IndicatorId { get; set; }

    public string? OptionName { get; set; }

    public int? Score { get; set; }

    public bool? RecordStatus { get; set; }

    public virtual Ppaindicator? Indicator { get; set; }
}
