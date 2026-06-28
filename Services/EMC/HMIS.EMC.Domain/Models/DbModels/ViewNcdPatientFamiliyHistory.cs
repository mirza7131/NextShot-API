using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class ViewNcdPatientFamiliyHistory
{
    public Guid? PatientVisitId { get; set; }

    public string Name { get; set; } = null!;

    public string? ShortName { get; set; }

    public string? Value { get; set; }
}
