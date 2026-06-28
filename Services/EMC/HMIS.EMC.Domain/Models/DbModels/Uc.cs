using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class Uc
{
    public int UcId { get; set; }

    public string? Name { get; set; }

    public string? TehsilCode { get; set; }
}
