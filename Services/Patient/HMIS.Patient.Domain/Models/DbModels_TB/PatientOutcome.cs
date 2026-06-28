using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientOutcome
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public string? Outcome { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public string? UpdatedDate { get; set; }

    public virtual Patient? Patient { get; set; }
}
