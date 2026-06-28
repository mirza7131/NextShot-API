using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class MlcsvinitialInfo
{
    public Guid MlcsvinitialInfoId { get; set; }

    public Guid? Mlcid { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? EmergencyNo { get; set; }

    public string? DaughterWifeOf { get; set; }

    public Guid? PatientImageId { get; set; }

    public Guid? PatientFingerPrintId { get; set; }

    public Guid? PatientSignatureId { get; set; }

    public Guid? GuardianSignatureId { get; set; }

    public Guid? PoliceSignatureId { get; set; }

    public Guid? CaseTypeProfileId { get; set; }

    public string? IncidentPlace { get; set; }

    public bool? IsBroughtDead { get; set; }

    public int? RefferToHealthFacilityId { get; set; }

    public DateTime? ArrivalDateTime { get; set; }

    public DateTime? ExaminationDateTime { get; set; }

    public string? AccompaniedBy { get; set; }

    public string? CourtOrder { get; set; }

    public string? NameOfOfficialAccompany { get; set; }

    public string? Mlcsvremark1 { get; set; }

    public string? Mlcsvremark2 { get; set; }

    public DateTime? AdmissionDateTime { get; set; }

    public DateTime? DischargeDateTime { get; set; }

    public bool? IsFinalReport { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
