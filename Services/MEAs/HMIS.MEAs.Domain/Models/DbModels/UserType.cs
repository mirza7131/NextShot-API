using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class UserType
{
    public int UserTypeId { get; set; }

    public string? UserTypeName { get; set; }
}
