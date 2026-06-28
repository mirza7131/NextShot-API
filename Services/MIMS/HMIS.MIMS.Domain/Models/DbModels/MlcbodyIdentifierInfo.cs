using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class MlcbodyIdentifierInfo
{
    public Guid MlcbodyIdentifierInfoId { get; set; }

    public Guid? Mlcid { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? IdentifierName { get; set; }

    public string? IdentifierCnic { get; set; }

    public Guid? IdentifierRelationTypeProfileId { get; set; }

    public string? IdentifierComments { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual Mlc? Mlc { get; set; }
}
