using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class IndentDetailLog
{
    public Guid IndentDetailId { get; set; }

    public Guid? IndentMasterId { get; set; }

    public long? BatchNo { get; set; }

    public long? MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public int? FundingSourceId { get; set; }

    public int? MedicineTypeId { get; set; }

    public DateTime? MedicineMfgDate { get; set; }

    public DateTime? MedicineExpDate { get; set; }

    public string? Remarks { get; set; }

    public decimal? RequestedQty { get; set; }

    public decimal? IssuedQty { get; set; }

    public decimal? ReceivedQty { get; set; }

    public decimal? UnitPrice { get; set; }

    public bool? IsSmlmedicine { get; set; }

    public bool? IsActive { get; set; }

    public string? ActionPhrase { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
