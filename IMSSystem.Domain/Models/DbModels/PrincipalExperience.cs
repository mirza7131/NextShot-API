using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class PrincipalExperience
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? JobTitle { get; set; }

    public string? Organization { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? UploadPath { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
