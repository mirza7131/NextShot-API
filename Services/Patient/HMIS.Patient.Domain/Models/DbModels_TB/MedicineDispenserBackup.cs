using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class MedicineDispenserBackup
{
    public int Id { get; set; }

    public bool? IsAdult { get; set; }

    public string? Tbtype { get; set; }

    public string? PatientType { get; set; }

    public int? TestId { get; set; }

    public string? TestResult { get; set; }

    public string? Phase { get; set; }

    public int? Month { get; set; }

    public int? MedicineId { get; set; }

    public decimal? WeightMin { get; set; }

    public decimal? WeightMax { get; set; }

    public decimal? MedicineCount { get; set; }
}
