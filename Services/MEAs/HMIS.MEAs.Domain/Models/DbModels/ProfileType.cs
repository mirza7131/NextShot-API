using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class ProfileType
{
    public int ProfileTypeId { get; set; }

    public string ProfileTypeName { get; set; } = null!;

    public string? ProfileTypeShortName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public Guid? UserLogId { get; set; }

    public int? ActionTypeId { get; set; }

    public virtual ICollection<Profile> Profiles { get; } = new List<Profile>();
}
