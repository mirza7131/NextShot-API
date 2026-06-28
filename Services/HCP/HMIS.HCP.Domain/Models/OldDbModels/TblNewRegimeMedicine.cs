using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblNewRegimeMedicine
{
    public int Id { get; set; }

    public string? MedicineName { get; set; }

    public string MedicineType { get; set; } = null!;
}
