using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class ViewPatientLabTestLabNo
{
    public string? LabNo { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
