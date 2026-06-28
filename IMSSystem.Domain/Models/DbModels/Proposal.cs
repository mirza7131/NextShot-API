using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Proposal
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? UserId { get; set; }

    public string? ImpactStatement { get; set; }

    public string? MeansOfVerification { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
