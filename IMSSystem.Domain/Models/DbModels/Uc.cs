using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Uc
{
    public int UcId { get; set; }

    public string? Name { get; set; }

    public string? TehsilCode { get; set; }
}
