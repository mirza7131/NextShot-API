using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class ViewHfDetail
{
    public int DivisionCode { get; set; }

    public string DivisionName { get; set; } = null!;

    public int DistrictCode { get; set; }

    public string DistrictName { get; set; } = null!;

    public int TehsilCode { get; set; }

    public string TehsilName { get; set; } = null!;

    public int HfCode { get; set; }

    public string HfName { get; set; } = null!;
}
