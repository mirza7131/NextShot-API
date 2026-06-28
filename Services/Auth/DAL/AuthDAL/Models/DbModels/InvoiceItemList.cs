using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class InvoiceItemList
{
    public Guid ItemListId { get; set; }

    public Guid? InvoiceMasterId { get; set; }

    public string? ItemName { get; set; }

    public int? Quantity { get; set; }

    public int? Rate { get; set; }

    public int? ItemTotalAmount { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DueAmountPayOn { get; set; }

    public Guid? DueAmountPayBy { get; set; }

    public virtual InvoiceMaster? InvoiceMaster { get; set; }
}
