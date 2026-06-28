using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Category
{
    public int CategoryId { get; set; }

    public int? ApplicationTypeId { get; set; }

    public int? ModuleId { get; set; }

    public int? SequenceNo { get; set; }

    public string? CategoryName { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public bool? IsRequired { get; set; }

    public virtual ApplicationType? ApplicationType { get; set; }

    public virtual ICollection<CategoryHfType> CategoryHfTypes { get; } = new List<CategoryHfType>();

    public virtual ICollection<CategoryShift> CategoryShifts { get; } = new List<CategoryShift>();

    public virtual ICollection<Indicator> Indicators { get; } = new List<Indicator>();

    public virtual ICollection<IndicatorsRecov> IndicatorsRecovs { get; } = new List<IndicatorsRecov>();

    public virtual Module? Module { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; } = new List<SubCategory>();
}
