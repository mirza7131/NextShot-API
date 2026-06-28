using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class InvoiceDocumentList
{
    public Guid InvoiceDocumentId { get; set; }

    public Guid? InvoiceMasterId { get; set; }

    public string? DocumentName { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual InvoiceMaster? InvoiceMaster { get; set; }
}
