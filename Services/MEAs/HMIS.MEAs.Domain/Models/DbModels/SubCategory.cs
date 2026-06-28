using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class SubCategory
{
    public int SubCategoryId { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public int CategoryId { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Indicator> Indicators { get; } = new List<Indicator>();

    public virtual ICollection<IndicatorsRecov> IndicatorsRecovs { get; } = new List<IndicatorsRecov>();
}
