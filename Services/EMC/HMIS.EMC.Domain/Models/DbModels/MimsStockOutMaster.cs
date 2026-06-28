using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class MimsStockOutMaster
{
    public Guid MimsStockOutMasterId { get; set; }

    public long? IndentByWardId { get; set; }

    public DateTime? StockOutDate { get; set; }

    public string? VoucherStatus { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Receiver { get; set; }

    public string? Rcnic { get; set; }

    public string? RmobileNo { get; set; }

    public string? Rdesignation { get; set; }

    public string? VehicleNo { get; set; }

    public bool? RecordStatus { get; set; }

    public int? Hfto { get; set; }

    public string? ReferenceNo { get; set; }

    public string? ReceivingVoucherStatus { get; set; }

    public bool? IsTransfer { get; set; }

    public bool? IsForAllocation { get; set; }

    public string? DriverName { get; set; }

    public string? DriverCnic { get; set; }

    public string? PlanNo { get; set; }

    public string? AuthorityLetterNo { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }
}
