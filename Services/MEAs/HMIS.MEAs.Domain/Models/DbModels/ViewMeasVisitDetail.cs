using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class ViewMeasVisitDetail
{
    public int? MonitoringMasterId { get; set; }

    public string? ModuleName { get; set; }

    public string? CategoryName { get; set; }

    public string? Subcategory { get; set; }

    public string? Indicator { get; set; }

    public string? Answer { get; set; }

    public int IndicatorId { get; set; }

    public int? ModuleId { get; set; }

    public int? SequenceNo { get; set; }

    public int? ParentIndicatorId { get; set; }
}
