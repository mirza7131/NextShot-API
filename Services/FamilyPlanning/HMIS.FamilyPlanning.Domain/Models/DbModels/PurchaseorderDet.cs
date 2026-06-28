using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class PurchaseorderDet
{
    public Guid PurchaseorderDetId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Hfmiscode { get; set; }

    public Guid? PurchaseorderMastId { get; set; }

    public Guid? Trno { get; set; }

    public Guid? GeoLevelsId { get; set; }

    public string? FkGeoLevels { get; set; }

    public string? RefDocNo { get; set; }

    public DateTime? RefDocDate { get; set; }

    public Guid? VendCatagoryId { get; set; }

    public Guid? VendorId { get; set; }

    public Guid? MedicineCategoryId { get; set; }

    public Guid? HrmedicineMasterId { get; set; }

    public Guid? MedicineBrandId { get; set; }

    public double? ContractRatePerAge { get; set; }

    public long? Quantity { get; set; }

    public double? Rate { get; set; }

    public double? GrossAmount { get; set; }

    public double? Gstrate { get; set; }

    public double? Gstamount { get; set; }

    public double? Whtrate { get; set; }

    public double? Whtamount { get; set; }

    public double? NetAmount { get; set; }

    public string? Behv { get; set; }

    public string? Remarks { get; set; }

    public int? SortOrder { get; set; }

    public string? EnableFlag { get; set; }

    public string? VocherStatus { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
