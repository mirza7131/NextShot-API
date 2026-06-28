using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? UserType { get; set; }

    public int? UserEntryId { get; set; }

    public int? IsBlocked { get; set; }

    public int OrderBy { get; set; }

    public int IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public string? UploadedDir { get; set; }

    public string? ImagePath { get; set; }
}
