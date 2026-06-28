using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class IndicatorOptionsFlood
{
    public int IndicatorOptionId { get; set; }

    public int? IndicatorId { get; set; }

    public string? Label { get; set; }

    public int? TypeId { get; set; }

    public int? InputTypeId { get; set; }

    public bool? IsOptionTotal { get; set; }

    public bool? IsOptionCalculation { get; set; }

    public bool? IsOptionEditable { get; set; }

    public bool? IsOptionTagged { get; set; }

    public string? DefaultValue { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsDeleted { get; set; }
}
