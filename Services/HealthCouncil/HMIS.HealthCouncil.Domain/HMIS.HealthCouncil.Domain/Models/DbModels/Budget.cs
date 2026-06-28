using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class Budget
{
    public Guid BudgetId { get; set; }

    public bool? IsBudgetAllocated { get; set; }

    public decimal? AllocatedAmount { get; set; }

    public bool? IsChequeIssue { get; set; }

    public decimal? ReleaseAmount { get; set; }

    public DateTime? ChequeIssueDate { get; set; }

    public string? ChequeImage { get; set; }

    public string? CourierCompany { get; set; }

    public DateTime? CourierDispatchDate { get; set; }

    public string? DiaryNo { get; set; }

    public int? ChequeNo { get; set; }

    public DateTime? ChequeDate { get; set; }

    public Guid? ChequeReceivedStatusProfileId { get; set; }

    public DateTime? ChequeReceivedDate { get; set; }

    public int? HealthFacilityId { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }
}
