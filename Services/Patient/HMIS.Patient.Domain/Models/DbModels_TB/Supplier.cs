using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Supplier
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? AlternatePhno { get; set; }

    public string? MobileNo { get; set; }

    public string? AlternateMobNumber { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? AlternateEmail { get; set; }

    public string? Address { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? HfmisCode { get; set; }

    public string? ContactPerson { get; set; }

    public string? CpPersonDesignation { get; set; }

    public DateTime? CpPersonDob { get; set; }

    public string? CpGender { get; set; }

    public string? CpPhoneNumber { get; set; }

    public string? CpAlternatePhno { get; set; }

    public string? CpMobileNo { get; set; }

    public string? CpAlternateMobNumber { get; set; }

    public string? CpFax { get; set; }

    public string? CpEmail { get; set; }

    public string? CpUrl { get; set; }

    public string? CpAlternateEmail { get; set; }

    public string? CpAddress { get; set; }

    public string? Remarks { get; set; }

    public int? SortOrder { get; set; }

    public string? EnableFlag { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }
}
