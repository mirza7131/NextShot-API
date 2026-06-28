using System;
using System.Collections.Generic;

namespace HMIS.Pathalogy.Domain.Models.DbModels;

public partial class ViewPatientLabTestLabNo
{
    public string? LabNo { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
