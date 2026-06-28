using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class Profile
{
    public int ProfileId { get; set; }

    public string ProfileName { get; set; } = null!;

    public string? ProfileShortName { get; set; }

    public int ProfileTypeId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public Guid? UserLogId { get; set; }

    public int? ActionTypeId { get; set; }

    public virtual ProfileType ProfileType { get; set; } = null!;
}
