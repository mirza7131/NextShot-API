using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class Vendor
{
    public Guid VendorId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Hfmiscode { get; set; }

    public string? Name { get; set; }

    public string? StoreName { get; set; }

    public Guid? VendorCatagoryId { get; set; }

    public Guid? PharmacistId { get; set; }

    public string? VendorPhoneNo { get; set; }

    public string? VendorAlternatePhno { get; set; }

    public string? VendorMobileNo { get; set; }

    public string? VendorAlternateMobNo { get; set; }

    public string? VendorFax { get; set; }

    public string? VendorEmail { get; set; }

    public string? VendorUrl { get; set; }

    public string? VendorAlternateEmail { get; set; }

    public string? VendorAddress { get; set; }

    public string? ContactPerson { get; set; }

    public string? CpersonDesignation { get; set; }

    public DateTime? CpersonDob { get; set; }

    public string? Cpgender { get; set; }

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

    public string? DslNo { get; set; }

    public DateTime? DslNoExpiry { get; set; }

    public Guid? VendorTypeProfileId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
