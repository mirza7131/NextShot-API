using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class ContignetStaff
{
    public Guid ContignetStaffId { get; set; }

    public string? Name { get; set; }

    public string? FatherName { get; set; }

    public string? PhoneNo { get; set; }

    public string? Cnic { get; set; }

    public decimal? Salary { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? ContractStartDate { get; set; }

    public DateTime? ContractExpiryDate { get; set; }

    public string? BankBranchName { get; set; }

    public string? BankBranchCode { get; set; }

    public string? BankAccountTitle { get; set; }

    public string? BankAccountNo { get; set; }

    public string? BankName { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public bool? IsActive { get; set; }

    public byte? ActionTypeId { get; set; }
}
