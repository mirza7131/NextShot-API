using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Artcenter
{
    public int? CenterTypeId { get; set; }

    public string? DistrictName { get; set; }

    public string? CentreType { get; set; }

    public int? CenterId { get; set; }

    public string? Title { get; set; }

    public int? Total { get; set; }
}
