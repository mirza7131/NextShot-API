using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class AssessmentMissingId
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public string SampleCollect { get; set; } = null!;

    public string PatientType { get; set; } = null!;

    public string? PatientStage { get; set; }

    public string? MrnNo { get; set; }

    public int? HospitalId { get; set; }

    public string? HfName { get; set; }

    public string? SampleNumber { get; set; }
}
