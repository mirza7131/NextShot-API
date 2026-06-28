using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class IntegratedRhc
{
    public int Id { get; set; }

    public string? Hfname { get; set; }

    public string? Dhis { get; set; }
}
