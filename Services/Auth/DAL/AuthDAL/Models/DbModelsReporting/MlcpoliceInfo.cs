using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class MlcpoliceInfo
{
    public Guid MlcpoliceInfoId { get; set; }

    public Guid? Mlcid { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? PoliceSignatureTypeProfileId { get; set; }

    public DateTime? PoliceSignatureDateTime { get; set; }

    public string? PolicePersonNameDesignation { get; set; }

    public string? PoliceStationName { get; set; }

    public string? PolicePeron2NameDesignation { get; set; }

    public string? PoliceStationAddress { get; set; }

    public string? CommentsByPolice { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
