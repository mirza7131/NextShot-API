using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblEvent
{
    public int Id { get; set; }

    public string? EventName { get; set; }

    public string? Industry { get; set; }

    public int? StartDate { get; set; }

    public int? EndDate { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int? TehsilId { get; set; }

    public int? HospitalId { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public int? Created { get; set; }

    public int? UpdatedBy { get; set; }

    public int? Updated { get; set; }
}
