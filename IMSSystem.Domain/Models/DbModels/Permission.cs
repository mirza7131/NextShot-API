using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Permission
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ImagePath { get; set; }

    public int? LinkId { get; set; }

    public string? LinkName { get; set; }

    public string? ActionMethodName { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? CreatedByUserId { get; set; }

    public bool? IsActive { get; set; }
}
