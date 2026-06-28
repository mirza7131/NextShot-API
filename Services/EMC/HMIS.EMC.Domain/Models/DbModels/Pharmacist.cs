using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class Pharmacist
{
    public Guid PharmacistId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Hfmiscode { get; set; }

    public string? Name { get; set; }

    public string? FatherName { get; set; }

    public string? Cnic { get; set; }

    public string? Gender { get; set; }

    public Guid? VendorId { get; set; }

    public string? PharmacistPhoneNo { get; set; }

    public string? PharmacistAlternatePhno { get; set; }

    public string? PharmacistMobileNo { get; set; }

    public string? PharmacistAlternateMobNo { get; set; }

    public string? PharmacistFax { get; set; }

    public string? PharmacistEmail { get; set; }

    public string? PharmacistUrl { get; set; }

    public string? PharmacistAlternateEmail { get; set; }

    public string? PharmacistAddress { get; set; }

    public string? ContactPerson { get; set; }

    public string? CpersonDesignation { get; set; }

    public DateTime? CpersonDob { get; set; }

    public string? CphoneNo { get; set; }

    public string? CalternatePhno { get; set; }

    public string? CmobileNo { get; set; }

    public string? CalternateMobNo { get; set; }

    public string? Cfax { get; set; }

    public string? Cemail { get; set; }

    public string? Curl { get; set; }

    public string? CalternateEmail { get; set; }

    public string? Craddress { get; set; }

    public string? Cpaddress { get; set; }

    public string? Remarks { get; set; }

    public int? SortOrder { get; set; }

    public string? CategoryANo { get; set; }

    public DateTime? CategoryARenewal { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
