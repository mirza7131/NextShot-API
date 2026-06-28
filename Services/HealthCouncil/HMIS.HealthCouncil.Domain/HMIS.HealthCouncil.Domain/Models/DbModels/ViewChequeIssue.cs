using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class ViewChequeIssue
{
    public Guid BudgetId { get; set; }

    public bool? IsChequeIssue { get; set; }

    public DateTime? ChequeIssueDate { get; set; }

    public decimal? ReleaseAmount { get; set; }

    public int? ChequeNo { get; set; }

    public string? CourierCompany { get; set; }

    public DateTime? CourierDispatchDate { get; set; }

    public string? DiaryNo { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? ChequeReceivedStatusProfileId { get; set; }

    public DateTime? ChequeReceivedDate { get; set; }

    public int? HealthFacilityId { get; set; }
}
