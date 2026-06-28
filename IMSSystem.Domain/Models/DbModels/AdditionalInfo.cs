using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class AdditionalInfo
{
    public int Id { get; set; }

    public int GrantApplicationId { get; set; }

    public string? UserId { get; set; }

    public string? AdditionalRevenueSource { get; set; }

    public string? OtherGrants { get; set; }

    public string? FinancialStream { get; set; }

    public int? Hear { get; set; }

    public string? Otherinfo { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
