using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class ViewPatientOpenVisitCountByGenderByMonth
{
    public string Gender { get; set; } = null!;

    public int? VisitCount { get; set; }

    public int? VisitMonth { get; set; }

    public int? HealthFacilityId { get; set; }
}
