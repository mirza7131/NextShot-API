using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class MedStock
{
    public Guid MedStockId { get; set; }

    public long? DepartmentId { get; set; }

    public long? FkmedId { get; set; }

    public long? Fkuom { get; set; }

    public long? MedicineId { get; set; }

    public int? HealthFacilityId { get; set; }

    public decimal? Qty { get; set; }

    public decimal? RemainingQty { get; set; }

    public string? AvailabilityStatus { get; set; }

    public string? PackageNo { get; set; }

    public string? BatchNo { get; set; }

    public string? FkhealthFacilityFrom { get; set; }

    public int? FkstockSource { get; set; }

    public string? Distributor { get; set; }

    public string? Manufacturer { get; set; }

    public DateTime? MfgDate { get; set; }

    public DateTime? ExpDate { get; set; }

    public DateTime? RecevingDate { get; set; }

    public DateTime? StockEntryDate { get; set; }

    public string? RecordStatus { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public string? Rmarks { get; set; }

    public string? DistrictCode { get; set; }

    public Guid? StockMasterId { get; set; }

    public string? PhysicalInspection { get; set; }

    public string? Dtlstatus { get; set; }

    public string? BatchStatus { get; set; }

    public decimal? PricePerItem1 { get; set; }

    public decimal? TotalPrice1 { get; set; }

    public decimal? TotalRaminigPrice1 { get; set; }

    public decimal? PricePerItem { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? TotalRaminigPrice { get; set; }

    public virtual ICollection<MedStockOut> MedStockOuts { get; } = new List<MedStockOut>();
}
