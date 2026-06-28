using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class HflistMode
{
    public int Id { get; set; }

    public string Hfmiscode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int Hfac { get; set; }

    public string? ImagePath { get; set; }

    public string UrlImagePath { get; set; } = null!;

    public string? DivisionCode { get; set; }

    public string DivisionName { get; set; } = null!;

    public string? DistrictCode { get; set; }

    public string DistrictName { get; set; } = null!;

    public string? TehsilCode { get; set; }

    public string TehsilName { get; set; } = null!;

    public string? CategoryCode { get; set; }

    public string HfcategoryName { get; set; } = null!;

    public string? HftypeCode { get; set; }

    public string HftypeName { get; set; } = null!;

    public int? OrderBy { get; set; }

    public long? EntityLifecycleId { get; set; }

    public string? PhoneNo { get; set; }

    public string? FaxNo { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Status { get; set; }

    public double? CoveredArea { get; set; }

    public double? UnCoveredArea { get; set; }

    public double? ResidentialArea { get; set; }

    public double? NonResidentialArea { get; set; }

    public string? Na { get; set; }

    public string? Pp { get; set; }

    public string? Mauza { get; set; }

    public string? UcName { get; set; }

    public string? UcNo { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? LastModifiedBy { get; set; }

    public string? UsersId { get; set; }

    public string? HfmisOldCode { get; set; }

    public string? ModeName { get; set; }
}
