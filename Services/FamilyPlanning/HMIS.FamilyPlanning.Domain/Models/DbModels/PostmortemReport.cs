using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class PostmortemReport
{
    public Guid PostmortemReportId { get; set; }

    public Guid? MlcpostmortemId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? ArticalToPolice { get; set; }

    public string? DoctorOpinion { get; set; }

    public string? ElapsedPortableTime { get; set; }

    public string? TimeBetweenInjuryAndDeath { get; set; }

    public string? TimeBetweenDeathAndPostmortem { get; set; }

    public string? LabExpertReport { get; set; }

    public string? FinalOpinion { get; set; }

    public string? PoliceOfficerName { get; set; }

    public string? PoliceOfficerMobileNo { get; set; }

    public DateTime? ReportHandoverDatetime { get; set; }

    public Guid? PoliceSignatureImageId { get; set; }

    public Guid? PoliceFingerPrintId { get; set; }

    public Guid? PatientManualReportId { get; set; }

    public Guid? PatientDrawImgId { get; set; }

    public bool? ChemicalExam { get; set; }

    public bool? Dnalab { get; set; }

    public bool? Histopathologist { get; set; }

    public bool? BallisticExpert { get; set; }

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
