using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class AspNetUser
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string? RawPassword { get; set; }

    public string? DistrictCode { get; set; }

    public string? DivisionCode { get; set; }

    public string? TehsilCode { get; set; }

    public string? FacilityCode { get; set; }

    public int ProgramId { get; set; }

    public string? Cnic { get; set; }

    public string? Designation { get; set; }

    public string? FullName { get; set; }

    public string? DoctorType { get; set; }

    public string? UserType { get; set; }

    public string? Address { get; set; }

    public string? Geolvl { get; set; }

    public string? HospitalName { get; set; }

    public string? RegistrationNo { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public DateTime? CreationDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; } = new List<AspNetUserToken>();

    public virtual ICollection<AspNetRole> Roles { get; } = new List<AspNetRole>();
}
