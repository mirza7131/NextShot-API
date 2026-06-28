using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ProposalProbe
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? UserId { get; set; }

    public int? ProposalId { get; set; }

    public int? ProbeId { get; set; }

    public string? ProbeName { get; set; }

    public string? Intervention { get; set; }

    public decimal? Cost { get; set; }

    public int? Time { get; set; }

    public string? OutCome { get; set; }

    public int? Days { get; set; }

    public string? ProposalReport { get; set; }

    public string? RiskStatement { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
