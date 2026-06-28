using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class DashboardCount
{
    public int? TotalPositiveCount { get; set; }

    public int? TotalNegativeCount { get; set; }
}
