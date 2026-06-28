using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class MedStockOut
{
    public Guid MedStockOutId { get; set; }

    public Guid? FkstockId { get; set; }

    public int? HealthFacilityId { get; set; }

    public long? AllocatedTo { get; set; }

    public string? BatchNo { get; set; }

    public decimal? Qty { get; set; }

    public string? HfmisCode { get; set; }

    public bool? RecordStatus { get; set; }

    public string? Comment { get; set; }

    public string? TransferStatus { get; set; }

    public int? MasterId { get; set; }

    public decimal? StockReceivedQty { get; set; }

    public string? Remarks { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual MedStock? Fkstock { get; set; }
}
