using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class SpInvoice
{
    public int InvoiceId { get; set; }

    public string? InvoiceNumber { get; set; }

    public Guid? SpId { get; set; }

    public int? HfId { get; set; }

    public Guid? ServiceTypeId { get; set; }

    public string? Month { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Comments { get; set; }

    public bool? IsActive { get; set; }

    public Guid? IssuedBy { get; set; }

    public DateTime? IssuedOn { get; set; }

    public bool? IsReIssued { get; set; }

    public Guid? ReIssuedBy { get; set; }

    public DateTime? ReIssuedOn { get; set; }

    public bool? IsApproved { get; set; }

    public Guid? ApprovedBy { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public bool? IsRejected { get; set; }

    public Guid? RejectedBy { get; set; }

    public DateTime? IsRejectedOn { get; set; }
}
