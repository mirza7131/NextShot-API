using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class TempMedicinePrice
{
    public int MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public decimal? UnitPrice { get; set; }
}
