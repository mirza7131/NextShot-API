using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class TempMimsmedicineListPrice
{
    public int MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public double? PricePerItem { get; set; }

    public int? MinimumLevel { get; set; }

    public string? MedicineType { get; set; }
}
