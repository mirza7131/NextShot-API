using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class ViewSocialWellfareDoctor
{
    public Guid UserId { get; set; }

    public string? FullName { get; set; }

    public string? FatherName { get; set; }

    public string Name { get; set; } = null!;

    public int? DistrictId { get; set; }
}
