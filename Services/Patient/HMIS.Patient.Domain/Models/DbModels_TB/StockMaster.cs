using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class StockMaster
{
    public int Id { get; set; }

    public int? SupplierId { get; set; }

    public int? ReferenceNumber { get; set; }

    public string? TransactionType { get; set; }

    public string? RecievingLocation { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? IssuedTo { get; set; }

    public string? ResponsiblePerson { get; set; }

    public string? RpDesignation { get; set; }

    public string? RpCnic { get; set; }

    public string? RpPhone { get; set; }

    public string? VehicleNumber { get; set; }

    public string? DriverName { get; set; }

    public int? SourceId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public int? TransactionId { get; set; }

    public int? VoucherId { get; set; }

    public string? VoucherStatus { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }

    public virtual StockSource? Source { get; set; }

    public virtual Voucher? Voucher { get; set; }
}
