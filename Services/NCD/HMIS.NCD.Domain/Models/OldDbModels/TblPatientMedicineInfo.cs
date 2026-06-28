using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblPatientMedicineInfo
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public string? PrescriptionNo { get; set; }

    public string? NewPrescriptionNo { get; set; }

    public int? AvailableDate { get; set; }

    public int? DeliveryDate { get; set; }

    public int? Created { get; set; }

    public string IsDemote { get; set; } = null!;
}
