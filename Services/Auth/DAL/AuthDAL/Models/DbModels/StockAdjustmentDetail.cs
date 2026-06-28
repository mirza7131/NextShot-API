using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class StockAdjustmentDetail
{
    public Guid StockAdjustmentDetailId { get; set; }

    public Guid? StockAdjustmentId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? MimsBranchId { get; set; }

    public int? MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public string? BatchNo { get; set; }

    public decimal? AvailableQuantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? CurrentUnitPrice { get; set; }

    public decimal? AvgUnitPrice { get; set; }

    public decimal? AdjustmentQuantity { get; set; }

    public Guid? ReasonProfileId { get; set; }

    public string? Remarks { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
