using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Adult
{
    public string? Id { get; set; }

    public string? IsAdult { get; set; }

    public string? Tbtype { get; set; }

    public string? PatientType { get; set; }

    public string? TestId { get; set; }

    public string? TestResult { get; set; }

    public string? Phase { get; set; }

    public string? Month { get; set; }

    public string? MedicineId { get; set; }

    public string? WeightMin { get; set; }

    public string? WeightMax { get; set; }

    public string? MedicineCount { get; set; }
}
