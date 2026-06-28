using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class UserLog
{
    public int UserLogId { get; set; }

    public int? UserId { get; set; }

    public int? ProvinceId { get; set; }

    public string? LocationCode { get; set; }

    public string? DepartmentName { get; set; }

    public string Username { get; set; } = null!;

    public string? FullName { get; set; }

    public string Password { get; set; } = null!;

    public string? ContactNo { get; set; }

    public string? Cnic { get; set; }

    public int? DesignationId { get; set; }

    public string? Designation { get; set; }

    public int? ZoneId { get; set; }

    public string? Email { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }
}
