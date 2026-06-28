using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class AppLocationsUc
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Ucnumber { get; set; }

    public string? Name { get; set; }

    public string? RecordStatus { get; set; }

    public virtual ICollection<PatientBackup6> PatientBackup6s { get; } = new List<PatientBackup6>();
}
