using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class ViewMlcdoctorList
{
    public Guid UserId { get; set; }

    public string? FullName { get; set; }

    public string? FatherName { get; set; }

    public int? HealthFacilityId { get; set; }

    public string Name { get; set; } = null!;

    public int? DistrictId { get; set; }
}
