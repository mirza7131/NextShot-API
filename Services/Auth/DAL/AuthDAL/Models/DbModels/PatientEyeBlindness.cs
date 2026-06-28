using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class PatientEyeBlindness
{
    public Guid PatientEyeBlindnessId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public string? ComorbidityBy { get; set; }

    public DateTime? DateOfInocvlation { get; set; }

    public string? ConsultantName { get; set; }

    public string? StatusOfVision { get; set; }

    public string? Recovery { get; set; }

    public int? HospitalInjectedId { get; set; }

    public int? HealthfacilityId { get; set; }

    public string? OtherHealthFacility { get; set; }

    public string? EyeInvolved { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? IsActive { get; set; }

    public byte? ActionTypeId { get; set; }
}
