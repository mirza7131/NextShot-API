using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class ViewPatientLabTestLabNo
{
    public string? LabNo { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
