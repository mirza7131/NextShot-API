using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class TblDistrictUsermap
{
    public int Id { get; set; }

    public string? DistrictName { get; set; }

    public string? DistrictCode { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
