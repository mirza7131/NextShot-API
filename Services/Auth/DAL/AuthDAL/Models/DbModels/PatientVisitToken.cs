using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class PatientVisitToken
{
    public Guid PatientVisitTokenId { get; set; }

    public int HealthFacilityId { get; set; }

    public int? DepartmentLookupId { get; set; }

    public DateTime? Date { get; set; }

    public int? TokenNo { get; set; }

    public string? FormattedTokenNo { get; set; }
}
