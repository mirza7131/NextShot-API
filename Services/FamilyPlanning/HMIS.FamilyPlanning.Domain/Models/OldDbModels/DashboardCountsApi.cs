using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class DashboardCountsApi
{
    public int Id { get; set; }

    public string? DistrictName { get; set; }

    public int? TotalRegistrationCount { get; set; }

    public int? TotalPreDiagnosedCount { get; set; }

    public int? TotalNewPatientCount { get; set; }

    public int? TotalNewAssessmentCount { get; set; }

    public int? TotalSamplesAccepted { get; set; }

    public int? TotalSamplesRejected { get; set; }

    public int? TotalSamplesResults { get; set; }

    public int? MedicineStockCounts { get; set; }

    public DateTime CreatedDate { get; set; }
}
