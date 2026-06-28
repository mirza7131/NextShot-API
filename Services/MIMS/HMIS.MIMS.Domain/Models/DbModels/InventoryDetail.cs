using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class InventoryDetail
{
    public Guid InventoryDetailId { get; set; }

    public Guid? InventoryMasterId { get; set; }

    public Guid? IndentMasterId { get; set; }

    public int? MedicineId { get; set; }

    public string? BatchNo { get; set; }

    public DateTime? MfgDate { get; set; }

    public DateTime? ExpDate { get; set; }

    public int? FundingSourceId { get; set; }

    public decimal? TotalQty { get; set; }

    public decimal? IssuedQty { get; set; }

    public decimal? AvailableQty { get; set; }

    public decimal? UnitPrice { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
