using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class NadraPatientImagesDetailView
{
    public Guid? PatientId { get; set; }

    public string? Base64 { get; set; }

    public string Name { get; set; } = null!;
}
