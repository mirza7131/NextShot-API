using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ProposalView
{
    public int ProposalId { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? ProposalUserId { get; set; }

    public string? ImpactStatement { get; set; }

    public string? MeansOfVerification { get; set; }

    public bool? ProposalIsActive { get; set; }

    public DateTime? ProposalCreatedAt { get; set; }

    public int ProposalProbeId { get; set; }

    public string? ProposalProbeUserId { get; set; }

    public int? ProposalId1 { get; set; }

    public int? ProbeId { get; set; }

    public string? ProbeName { get; set; }

    public string? Intervention { get; set; }

    public decimal? Cost { get; set; }

    public int? Time { get; set; }

    public string? OutCome { get; set; }

    public int? Days { get; set; }

    public string? ProposalReport { get; set; }

    public string? RiskStatement { get; set; }

    public bool? ProposalProbeIsActive { get; set; }

    public DateTime? ProposalProbeCreatedAt { get; set; }
}
