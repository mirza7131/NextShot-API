using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class ViewEyeBlindnesspatient
{
    public string? FullName { get; set; }

    public int? Age { get; set; }

    public string? ComorbidityBy { get; set; }

    public DateTime? DateOfInocvlation { get; set; }

    public string? ConsultantName { get; set; }

    public string? StatusOfVision { get; set; }

    public string? Recovery { get; set; }

    public string? HealthFacility { get; set; }

    public string? InjectedHospital { get; set; }

    public string? OtherHealthFacility { get; set; }

    public string? EyeInvolved { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? Createdby { get; set; }
}
