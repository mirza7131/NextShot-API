using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class ViewDivisionWiseDiseaseCount
{
    public Guid PatientOpenVisitId { get; set; }

    public string? Division { get; set; }

    public string? District { get; set; }

    public string? Tehsil { get; set; }

    public string? HealthFacility { get; set; }

    public Guid PatientId { get; set; }

    public string? PatientName { get; set; }

    public Guid PatientDiagnoseId { get; set; }

    public string DiseaseName { get; set; } = null!;

    public DateTime? Date { get; set; }
}
