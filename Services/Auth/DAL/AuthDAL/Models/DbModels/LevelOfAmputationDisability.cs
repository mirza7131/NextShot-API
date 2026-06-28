using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class LevelOfAmputationDisability
{
    public Guid LevelOfAmputationDisabilityId { get; set; }

    public Guid? ProstheticId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public string? OtherDisability { get; set; }

    public int? FootSize { get; set; }

    public int? StumpLength { get; set; }

    public string? ElbowCenterToUlnarStyloid { get; set; }

    public int? Max { get; set; }

    public int? Min { get; set; }

    public string? ColorCode { get; set; }

    public Guid? SideProfileId { get; set; }

    public string? OtherPrescription { get; set; }

    public bool IsActive { get; set; }

    public int ActionTypeId { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }
}
