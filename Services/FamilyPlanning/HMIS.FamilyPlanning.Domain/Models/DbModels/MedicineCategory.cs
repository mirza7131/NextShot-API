using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class MedicineCategory
{
    public long Id { get; set; }

    public string? CategoryName { get; set; }

    public string? Manufacturer { get; set; }

    public double? RatePerAge { get; set; }

    public string? Remarks { get; set; }

    public string? EnableFlag { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
