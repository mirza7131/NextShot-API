using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class InvoiceMaster
{
    public Guid InvoiceMasterId { get; set; }

    public string? InvoiceNumber { get; set; }

    public string? BillToName { get; set; }

    public string? PrintBy { get; set; }

    public int? ApplyDiscount { get; set; }

    public int? ReceivedAmount { get; set; }

    public int? HealthfacilityId { get; set; }

    public int? TotalAmount { get; set; }

    public int? DueAmount { get; set; }

    public int? NetAmount { get; set; }

    public string? TermAndCondition { get; set; }

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

    public virtual ICollection<InvoiceDocumentList>? InvoiceDocumentLists { get; set; } = new List<InvoiceDocumentList>();

    public virtual ICollection<InvoiceItemList>? InvoiceItemLists { get; set; } = new List<InvoiceItemList>();
}
