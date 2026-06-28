using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Building
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? Name { get; set; }

    public decimal? Length { get; set; }

    public decimal? Width { get; set; }

    public string? ImagePath { get; set; }

    public int? BuildingTypeId { get; set; }

    public string? BuilidingType { get; set; }

    public int? Students { get; set; }

    public string? LabPurpose { get; set; }

    public string? AvailableEquipment { get; set; }

    public int? Occupancy { get; set; }

    public int? MultiMediaProvisions { get; set; }

    public int? Rooms { get; set; }

    public int? FurniturePerRoom { get; set; }

    public int? RoomOccupancy { get; set; }

    public bool? MessCommonArea { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
