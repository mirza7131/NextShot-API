using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class Expense
{
    public Guid ExpenseId { get; set; }

    public Guid? MeetingDetailId { get; set; }

    public Guid? MeetingDisscussedCategoryId { get; set; }

    public Guid? VendorId { get; set; }

    public string? ChequeNo { get; set; }

    public DateTime? ChequeDate { get; set; }

    public int? ExpenseAmount { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? ExpenseFormJson { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual MeetingDetail? MeetingDetail { get; set; }

    public virtual MeetingDisscussedCategory? MeetingDisscussedCategory { get; set; }

    public virtual Vendor? Vendor { get; set; }
}
