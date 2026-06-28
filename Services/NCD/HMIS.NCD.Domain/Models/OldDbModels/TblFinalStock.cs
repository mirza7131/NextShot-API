using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblFinalStock
{
    public int Id { get; set; }

    public string? HealthcareFacility { get; set; }

    public int? MedicineId { get; set; }

    public string? Districts { get; set; }

    public double? SdStockEntry { get; set; }

    public int RemainingStock { get; set; }

    public int? HospitalId { get; set; }

    public int? District { get; set; }

    public int? Tehsil { get; set; }

    public int? RemainingStock2 { get; set; }
}
