using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class MentalHealthPatientDetail
{
    public Guid MentalHealthPatientDetailId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public int? HraAnxietyTotalScore { get; set; }

    public string? HraAnxietyRiskStatus { get; set; }

    public int? HraDepressionTotalScore { get; set; }

    public string? HraDepressionRiskStatus { get; set; }

    public int? DaAnxietyTotalScore { get; set; }

    public string? DaAnxietyRiskStatus { get; set; }

    public int? DaDepressionTotalScore { get; set; }

    public string? DaDepressionRiskStatus { get; set; }

    public string? DiagnosticDisease { get; set; }

    public Guid? ReferredBy { get; set; }

    public string? ReferredTo { get; set; }

    public DateTime? ReferredDate { get; set; }

    public int? TotalNoofFollowups { get; set; }

    public bool? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? HraDate { get; set; }

    public DateTime? DaAnxietyDate { get; set; }

    public DateTime? DaDepressionDate { get; set; }

    public string? DaAnxietyHfmiscode { get; set; }

    public Guid? DaAnxietyCreatedBy { get; set; }

    public string? DaDepressionHfmiscode { get; set; }

    public Guid? DaDepressionCreatedBy { get; set; }

    public string? HraMessage { get; set; }

    public string? DaAnxietyMessage { get; set; }

    public string? DaDepressionMessage { get; set; }

    public string? ReferredHfmiscode { get; set; }

    public string? TreatmentOutCome { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }

    public Guid? LastFollowId { get; set; }

    public bool? IsPatientCounciled { get; set; }
}
