using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class BasicMedicine
{
    public Guid Id { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Hfmiscode { get; set; }

    public string? Name { get; set; }

    public string? Potency { get; set; }

    public Guid? MedicineTypeId { get; set; }

    public Guid? MedicineCategoryId { get; set; }

    public string? Description { get; set; }

    public string? GenericFormula { get; set; }

    public Guid? MedicineBrandId { get; set; }

    public string? Strength { get; set; }

    public string? PackSize { get; set; }

    public string? ManufacturedBy { get; set; }

    public double? Rate { get; set; }

    public string? Remarks { get; set; }

    public bool? IsSmlmedicine { get; set; }

    public string? EnableFlag { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? MedicineModule { get; set; }

    public int? MinimumLevel { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
