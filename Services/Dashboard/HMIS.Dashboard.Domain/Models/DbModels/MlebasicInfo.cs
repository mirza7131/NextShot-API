using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class MlebasicInfo
{
    public Guid MlebasicInfoId { get; set; }

    public Guid Mlcid { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? PatientImageProfileId { get; set; }

    public Guid? PoliceSignatureProfileId { get; set; }

    public Guid? PatientSignatureProfileId { get; set; }

    public Guid? AdultOrUnderAgeFingerPrintProfileId { get; set; }

    public Guid? PoliceFingerPrintProfileId { get; set; }

    public DateTime? Dob { get; set; }

    public string? Relation { get; set; }

    public DateTime? MlcDate { get; set; }

    public int? RefferToHealthFacilityId { get; set; }

    public Guid? CaseTypeProfileId { get; set; }

    public string? IncidentPlace { get; set; }

    public string? MlcRemark1 { get; set; }

    public string? MlcRemark2 { get; set; }

    public string? AccompaniesBy { get; set; }

    public DateTime? ArrivalDateTime { get; set; }

    public DateTime? ExaminationDateTime { get; set; }

    public string? CourtOrder { get; set; }

    public string? PoliceConstableName { get; set; }

    public string? PoliceConstablePhoneNumber { get; set; }

    public DateTime? AdmitDateTime { get; set; }

    public DateTime? DischargeDateTime { get; set; }

    public string? Comments { get; set; }

    public DateTime? SentDateTime { get; set; }

    public long? ReportCounts { get; set; }

    public bool? IsFinalReport { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
