using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class MeasBundal
{
    public int Id { get; set; }

    public string? DivisionCode { get; set; }

    public string? DivisionName { get; set; }

    public string? DistrictCode { get; set; }

    public string? DistrictName { get; set; }

    public string? Month { get; set; }

    public int? Year { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }
}
