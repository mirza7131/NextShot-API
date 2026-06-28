using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ViewInvoiceStatusCount
{
    public int? TotalActive { get; set; }

    public int? PendingCount { get; set; }

    public int? ApprovedCount { get; set; }

    public int? ReissuedCount { get; set; }

    public int? RejectedCount { get; set; }
}
