using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Opdstat
{
    public int Id { get; set; }

    public int? Opdcount { get; set; }

    public DateTime CreationDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? CreatedBy { get; set; }

    public string? Hfcode { get; set; }
}
