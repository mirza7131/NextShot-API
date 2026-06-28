using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Voucher
{
    public int Id { get; set; }

    public string? VoucherType { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletionDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public virtual ICollection<Adjustment> Adjustments { get; } = new List<Adjustment>();

    public virtual ICollection<StockDetail> StockDetails { get; } = new List<StockDetail>();

    public virtual ICollection<StockMaster> StockMasters { get; } = new List<StockMaster>();
}
