using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Indicator
{
    public int IndicatorId { get; set; }

    public int? SequenceNo { get; set; }

    public string? Question { get; set; }

    public int? IndicatorTypeId { get; set; }

    public int? ParentIndicatorId { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public string? FormId { get; set; }

    public int? ShowInCase { get; set; }

    public bool? IsRemarksShow { get; set; }

    public int? RemarksShowInCase { get; set; }

    public bool? IsRemarksMandatoy { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? ApplicationTypeId { get; set; }

    public int? ModuleId { get; set; }

    public int? InputTypeId { get; set; }

    public bool? IsOptionTotal { get; set; }

    public bool? IsOptionCalculation { get; set; }

    public bool? IsOptionEditable { get; set; }

    public bool? IsOptionTagged { get; set; }

    public string? DefaultValue { get; set; }

    public bool? IsRequired { get; set; }

    public bool? IsCalculation { get; set; }

    public bool? IsPhysicalView { get; set; }

    public string? ShortQuestion { get; set; }

    public int? ReportSequence { get; set; }

    public bool? ReportActive { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<IndicatorHealthFacility> IndicatorHealthFacilities { get; } = new List<IndicatorHealthFacility>();

    public virtual ICollection<IndicatorHf> IndicatorHfs { get; } = new List<IndicatorHf>();

    public virtual ICollection<IndicatorOption> IndicatorOptions { get; } = new List<IndicatorOption>();

    public virtual ICollection<IndicatorShift> IndicatorShifts { get; } = new List<IndicatorShift>();

    public virtual OptionType? IndicatorType { get; set; }

    public virtual InputType? InputType { get; set; }

    public virtual ICollection<MonitoringChild> MonitoringChildren { get; } = new List<MonitoringChild>();

    public virtual SubCategory? SubCategory { get; set; }
}
