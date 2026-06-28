using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class StockDetail
{
    public int Id { get; set; }

    public int? StockMasterId { get; set; }

    public int? MedicineId { get; set; }

    public string? Batch { get; set; }

    public int? Quantity { get; set; }

    public string? Action { get; set; }

    public DateTime? ManufacturingDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int? AvailableQuantity { get; set; }

    public string? Remarks { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public int? VoucherId { get; set; }

    public int? AdjustmentType { get; set; }

    public int? Uomid { get; set; }

    public string? Geolvl { get; set; }

    public string? FacilityCode { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual UnitofMeasurement? Uom { get; set; }

    public virtual Voucher? Voucher { get; set; }
}
