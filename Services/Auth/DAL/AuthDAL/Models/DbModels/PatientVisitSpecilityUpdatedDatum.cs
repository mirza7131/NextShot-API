using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class PatientVisitSpecilityUpdatedDatum
{
    public int PatientVisitSpecilityUpdatedDataId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientId { get; set; }

    public int? CurrentSectionLookupId { get; set; }

    public int? UpdatedSectionLookupId { get; set; }

    public DateTime? CreatedOn { get; set; }
}
