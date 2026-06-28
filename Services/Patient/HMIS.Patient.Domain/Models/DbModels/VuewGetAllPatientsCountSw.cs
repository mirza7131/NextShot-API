using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class VuewGetAllPatientsCountSw
{
    public string? PatientDistrictName { get; set; }

    public string? PatientDivisionName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? TotalPatients { get; set; }
}
