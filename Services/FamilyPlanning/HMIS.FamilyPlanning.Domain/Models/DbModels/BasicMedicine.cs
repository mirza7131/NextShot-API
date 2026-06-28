using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class BasicMedicine
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Potency { get; set; }

    public long? MedicineTypeId { get; set; }

    public long? MedicineCategoryId { get; set; }

    public string? Description { get; set; }

    public string? GenericFormula { get; set; }

    public long? MedicineBrandId { get; set; }

    public string? Strength { get; set; }

    public string? PackSize { get; set; }

    public string? ManufacturedBy { get; set; }

    public double? Rate { get; set; }

    public string? Remarks { get; set; }

    public bool? IsSmlmedicine { get; set; }

    public string? EnableFlag { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? MedicineModule { get; set; }

    public int? MinimumLevel { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
