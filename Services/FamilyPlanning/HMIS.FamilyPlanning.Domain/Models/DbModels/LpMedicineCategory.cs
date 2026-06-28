using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class LpMedicineCategory
{
    public Guid Id { get; set; }

    public string? CategoryName { get; set; }

    public string? Manufacturer { get; set; }

    public double? RatePerAge { get; set; }

    public string? Remarks { get; set; }

    public string? EnableFlag { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeleteBy { get; set; }

    public byte ActionTypeId { get; set; }
}
