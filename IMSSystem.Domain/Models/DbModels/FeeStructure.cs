using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class FeeStructure
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? FeeType { get; set; }

    public decimal? Amount { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
