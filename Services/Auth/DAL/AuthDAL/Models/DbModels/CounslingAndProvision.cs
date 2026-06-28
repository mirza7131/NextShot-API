using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class CounslingAndProvision
{
    public Guid CounslingAndProvisionalId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public bool IsUseFpwheelCard { get; set; }

    public bool IsCounselThePatient { get; set; }

    public Guid MethodProposedProfileId { get; set; }

    public Guid MethodClientProfileId { get; set; }

    public Guid MethodAdoptedProfileId { get; set; }

    public Guid ReasonProfileId { get; set; }

    public string? Remarks { get; set; }

    public DateTime FollowUpVisitDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public bool IsActive { get; set; }

    public byte ActionTypeId { get; set; }
}
