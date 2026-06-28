using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class ViewPatientLabTestLabNo
{
    public string? LabNo { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
