using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class ViewAllocateBudget
{
    public Guid BudgetId { get; set; }

    public bool? IsChequeIssue { get; set; }

    public bool? IsBudgetAllocated { get; set; }

    public decimal? AllocatedAmount { get; set; }

    public int? HealthFacilityId { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? BankContact { get; set; }

    public string? Bank { get; set; }

    public string? BranchName { get; set; }

    public string? BranchCode { get; set; }

    public string? AccountNo { get; set; }

    public string? AccountTitle { get; set; }

    public decimal? CurrentBalance { get; set; }

    public decimal? OpeningBalance { get; set; }

    public Guid? ChequeReceivedStatusProfileId { get; set; }
}
