using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblMedicinesRecommended
{
    public int Id { get; set; }

    public string? MedicineName { get; set; }

    public string? MedicineType { get; set; }

    public string? LeafletColor { get; set; }
}
