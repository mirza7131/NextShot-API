using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class TblShcuserMap
{
    public int Id { get; set; }

    public string? MeaType { get; set; }

    public string? Region { get; set; }

    public string? Division { get; set; }

    public string? Name { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
