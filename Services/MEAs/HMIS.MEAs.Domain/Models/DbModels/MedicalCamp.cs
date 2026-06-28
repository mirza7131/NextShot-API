using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class MedicalCamp
{
    public int CampId { get; set; }

    public string? CampName { get; set; }

    public string? Site { get; set; }

    public string? DistrictCode { get; set; }

    public string? DistrictName { get; set; }

    public string? TehsilCode { get; set; }

    public string? TehsilName { get; set; }

    public int? ShiftId { get; set; }

    public bool? IsActive { get; set; }
}
