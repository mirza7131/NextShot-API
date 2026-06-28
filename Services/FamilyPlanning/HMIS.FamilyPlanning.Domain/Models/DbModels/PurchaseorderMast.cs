using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class PurchaseorderMast
{
    public Guid Id { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Hfmiscode { get; set; }

    public Guid? VoucherTypeId { get; set; }

    public string? VoucherType { get; set; }

    public Guid? GeoLevelsId { get; set; }

    public string? FkGeoLevels { get; set; }

    public DateTime? EffectiveFromDate { get; set; }

    public DateTime? EffectiveToDate { get; set; }

    public string? RefDocNo { get; set; }

    public DateTime? RefDocDate { get; set; }

    public Guid? VendorCatagoryId { get; set; }

    public Guid? VendorId { get; set; }

    public Guid? FromMedicineCategoryId { get; set; }

    public Guid? ToMedicineCategoryId { get; set; }

    public Guid? HrmedicineMasterId { get; set; }

    public Guid? MedicineBrandId { get; set; }

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
