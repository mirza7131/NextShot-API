using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Finance
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? ApprovalMechanism { get; set; }

    public decimal? AnnualExpenditures { get; set; }

    public string? AuditReport { get; set; }

    public string? FinancialStatement { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
