using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class InventoryMaster
{
    public Guid InventoryMasterId { get; set; }

    public int? MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public int? MedicineTypeId { get; set; }

    public Guid? MimsBranchId { get; set; }

    public int? HealthfacilityId { get; set; }

    public decimal? TotalQty { get; set; }

    public decimal? IssuedQty { get; set; }

    public decimal? LockedQty { get; set; }

    public decimal? AdjustmentQty { get; set; }

    public decimal? BatchHoldQty { get; set; }

    public bool? IsBatchOnHold { get; set; }

    public decimal? AvailableQty { get; set; }

    public decimal? PerPackQty { get; set; }

    public decimal? PerPackAvailableQty { get; set; }

    public decimal? CurrentUnitPrice { get; set; }

    public decimal? AvgUnitPrice { get; set; }

    public bool? IsSmlmedicine { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
