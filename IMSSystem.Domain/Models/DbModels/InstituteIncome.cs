using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class InstituteIncome
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public decimal? IncomeTotal { get; set; }

    public string? IncomeSource { get; set; }

    public string? IncomeStatement { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
