using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Governance
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public bool? BoardAvailable { get; set; }

    public string? PostingMethod { get; set; }

    public string? RecruitingMethod { get; set; }

    public bool? Evaluation { get; set; }

    public string? EvaluationReport { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
