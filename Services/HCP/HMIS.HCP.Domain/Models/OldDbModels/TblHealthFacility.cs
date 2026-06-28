using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblHealthFacility
{
    public int Id { get; set; }

    public string HfName { get; set; } = null!;

    public int? Category { get; set; }

    public int? Type { get; set; }

    public int HfTypeCode { get; set; }

    public int HfRegionCode { get; set; }

    public string ResponsibleUser { get; set; } = null!;

    public string Hfac { get; set; } = null!;

    public int DivisionCode { get; set; }

    public int DistrictCode { get; set; }

    public int TehsilCode { get; set; }

    public int Province { get; set; }

    public string? IsActive { get; set; }

    public string? Identifier { get; set; }

    public int UserId { get; set; }

    public string? MoPosition { get; set; }

    public int? CreatedBy { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public int? UpdatedBy { get; set; }
}
