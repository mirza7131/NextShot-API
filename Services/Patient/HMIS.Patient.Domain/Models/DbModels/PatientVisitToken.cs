using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class PatientVisitToken
{
    public Guid PatientVisitTokenId { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? Date { get; set; }

    public int? TokenNo { get; set; }
}
