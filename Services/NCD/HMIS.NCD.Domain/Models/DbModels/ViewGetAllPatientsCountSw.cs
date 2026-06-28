using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class ViewGetAllPatientsCountSw
{
    public int PatientDistrctId { get; set; }

    public string? PatientDistrictName { get; set; }

    public int PatientDivisionId { get; set; }

    public string? PatientDivisionName { get; set; }

    public int? ProvinceId { get; set; }

    public int HealthFacilityId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? TotalPatients { get; set; }
}
