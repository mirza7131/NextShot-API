using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class AspNetRole
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public int ProgramId { get; set; }

    public int CategoryId { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; } = new List<AspNetRoleClaim>();

    public virtual ICollection<AspNetUser> Users { get; } = new List<AspNetUser>();
}
