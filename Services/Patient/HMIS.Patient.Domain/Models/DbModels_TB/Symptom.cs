using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Symptom
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? RecordStatus { get; set; }
}
