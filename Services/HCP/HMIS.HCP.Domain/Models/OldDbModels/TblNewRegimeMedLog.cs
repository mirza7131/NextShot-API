using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblNewRegimeMedLog
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? HospitalId { get; set; }

    public string? BaselineType { get; set; }

    public int? NoOfDosage { get; set; }

    public int? MedicineId { get; set; }

    public string? IsDemote { get; set; }

    public int? Created { get; set; }
}
