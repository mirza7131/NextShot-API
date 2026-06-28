using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class TempMedicinePrice
{
    public int MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public decimal? UnitPrice { get; set; }
}
