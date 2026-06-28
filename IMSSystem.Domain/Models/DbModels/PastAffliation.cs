using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class PastAffliation
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? OrganizationName { get; set; }

    public DateTime? AffliationStartDate { get; set; }

    public DateTime? AffliationEndDate { get; set; }

    public string? Certificate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
