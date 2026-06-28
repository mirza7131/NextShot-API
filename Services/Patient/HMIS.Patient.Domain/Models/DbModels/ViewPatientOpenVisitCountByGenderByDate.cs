using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class ViewPatientOpenVisitCountByGenderByDate
{
    public string Gender { get; set; } = null!;

    public int? VisitCount { get; set; }

    public DateTime? VisitDate { get; set; }

    public int? HealthFacilityId { get; set; }
}
