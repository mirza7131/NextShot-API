using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public int AccessFailedCount { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public bool LockoutEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public string? NormalizedEmail { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string? SecurityStamp { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public string? UserName { get; set; }

    public string? Cnic { get; set; }

    public int? ProfileId { get; set; }

    public int? UserId { get; set; }

    public string? Domiclie { get; set; }

    public string? DivisionId { get; set; }

    public string? DistrictId { get; set; }

    public string? TehsilId { get; set; }

    public string? Hfmiscode { get; set; }

    public string? LevelId { get; set; }

    public string? DesigCode { get; set; }

    public bool? IsActive { get; set; }

    public string? HfmisCodeNew { get; set; }

    public DateTime? CreationDate { get; set; }

    public decimal? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public decimal? ModifiedBy { get; set; }

    public string? UserDetail { get; set; }

    public string? Responsibleuser { get; set; }

    public string? Hashynoty { get; set; }

    public string? HfTypeCode { get; set; }

    public DateTime? LockoutEndDateUtc { get; set; }

    public bool? IsUpdated { get; set; }

    public string? RegisterThumb { get; set; }

    public bool? IsRegistered { get; set; }

    public string? Hfname { get; set; }

    public string? DivisionCode { get; set; }

    public string? DistrictCode { get; set; }

    public string? TehsilCode { get; set; }

    public string? InstitutionAdress { get; set; }

    public string? InstitutionName { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
