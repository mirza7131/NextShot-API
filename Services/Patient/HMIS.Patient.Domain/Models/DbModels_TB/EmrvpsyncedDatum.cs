using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class EmrvpsyncedDatum
{
    public int Id { get; set; }

    public bool? IsSynced { get; set; }

    public int? PatientId { get; set; }

    public DateTime? CreatedAt { get; set; }
}
