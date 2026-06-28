using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class PrincipalQualification
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public int? QualificationId { get; set; }

    public string? QualificationName { get; set; }

    public string? UploadPath { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
