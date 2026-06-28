using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblTehsil
{
    public int Id { get; set; }

    public string TehsilName { get; set; } = null!;

    public int TehsilCode { get; set; }

    public int DistrictCode { get; set; }

    public int DivisionCode { get; set; }

    public string? DestinationCode { get; set; }

    public string? IsActive { get; set; }
}
