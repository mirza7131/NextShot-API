using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class ViewReleaseBudget
{
    public Guid BudgetId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? ChequeImage { get; set; }

    public bool? BankAccountStatus { get; set; }

    public decimal? AllocatedAmount { get; set; }

    public decimal? ReleaseAmount { get; set; }

    public bool? IsChequeIssue { get; set; }

    public DateTime? CreatedOn { get; set; }
}
