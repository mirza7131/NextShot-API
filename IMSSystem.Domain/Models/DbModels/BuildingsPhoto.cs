using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class BuildingsPhoto
{
    public int Id { get; set; }

    public int? BuildingId { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? ImagePath { get; set; }

    public bool? IsActive { get; set; }
}
