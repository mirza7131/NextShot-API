using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class SiteSead
{
    public int Id { get; set; }

    public string SiteTitle { get; set; } = null!;

    public string SiteTitleShort { get; set; } = null!;

    public int PageSize { get; set; }
}
