using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class MimsStockMaster
{
    public Guid MimsStockMasterId { get; set; }

    public string? ReceiptNo { get; set; }

    public string? ReferenceNo { get; set; }

    public DateTime? ReceivingTime { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? FundingSourceName { get; set; }

    public int? FundingSourceId { get; set; }

    public bool? RecordStatus { get; set; }

    public string? VoucherType { get; set; }

    public string? VoucherStatus { get; set; }

    public string? DistrictCode { get; set; }

    public int? MedStockOutMasterId { get; set; }

    public int? LpmasterId { get; set; }

    public string? VerticalProgram { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }
}
