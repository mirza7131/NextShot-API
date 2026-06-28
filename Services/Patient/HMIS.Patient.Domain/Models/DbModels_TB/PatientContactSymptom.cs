using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientContactSymptom
{
    public int Id { get; set; }

    public int? PatientContactId { get; set; }

    public string? Symptom { get; set; }

    public virtual PatientContact? PatientContact { get; set; }
}
