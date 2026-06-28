using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class UserRegistrationLog
{
    public Guid UserId { get; set; }

    public int? ProvinceId { get; set; }

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int? TehsilId { get; set; }

    public int? UcId { get; set; }

    public string? FullName { get; set; }

    public string? FatherName { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? ContactNo { get; set; }

    public string? ProfilePic { get; set; }

    public string? Cnic { get; set; }

    public int? HrId { get; set; }

    public int? HealthFacilityId { get; set; }

    public int? DepartmentId { get; set; }

    public int? SectionId { get; set; }

    public string? CurrentGradeBps { get; set; }

    public DateTime? Dob { get; set; }

    public Guid? GenderProfileId { get; set; }

    public bool? IsActive { get; set; }

    public Guid? DesignationProfileId { get; set; }

    public Guid? UserTypeProfileId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public DateTime? ActionDate { get; set; }

    public bool? PmisUser { get; set; }

    public bool? IsShowRoleOnly { get; set; }

    public bool? IsUserLoginFirstTime { get; set; }

    public DateTime? PasswordChangedOn { get; set; }
}
