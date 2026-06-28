using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Scholarship
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? ScholarshipType { get; set; }

    public string? GrantName { get; set; }

    public decimal? Amount { get; set; }

    public string? Mechanism { get; set; }

    public int? NoOfStudents { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
