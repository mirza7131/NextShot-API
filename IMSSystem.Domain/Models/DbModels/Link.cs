using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Link
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public string? Icon { get; set; }

    public string? Badge { get; set; }

    public string? SerialNo { get; set; }

    public bool? IsActive { get; set; }
}
