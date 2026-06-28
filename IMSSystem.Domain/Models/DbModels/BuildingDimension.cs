using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class BuildingDimension
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public int? BuildingStatusId { get; set; }

    public string? BuildingStatus { get; set; }

    public decimal? TotalArea { get; set; }

    public decimal? TotalCoveredArea { get; set; }

    public decimal? Floors { get; set; }

    public string? Map { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
