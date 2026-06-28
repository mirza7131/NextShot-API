using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class User
{
    public int UserId { get; set; }

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

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public int? UserTypeId { get; set; }

    public int? RegionId { get; set; }

    public virtual ICollection<UserLocation> UserLocations { get; } = new List<UserLocation>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();

    public virtual Zone? Zone { get; set; }
}
