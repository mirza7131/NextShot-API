using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class HealthFacilityBankDetail
{
    public Guid HealthFacilityBankDetailId { get; set; }

    public string? Bank { get; set; }

    public string? BranchName { get; set; }

    public string? BranchCode { get; set; }

    public string? AccountTitle { get; set; }

    public string? AccountNo { get; set; }

    public string? BankContact { get; set; }

    public decimal? CurrentBalance { get; set; }

    public decimal? OpeningBalance { get; set; }

    public int? HealthFacilityId { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
