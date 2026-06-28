using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class PatientFollowUp
{
    public Guid PatientFollowUpsId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int HealthFacilityId { get; set; }

    public DateTime? NextFollowUpDate { get; set; }

    public DateTime? PreviousFollowUpdate { get; set; }

    public DateTime? LastIssueBookLetDateTime { get; set; }

    public string? LipidProfile { get; set; }

    public string? HbA1c { get; set; }

    public double? HbA1cpercent { get; set; }

    public int? CholesterolMgDl { get; set; }

    public int? TriGlycerideMgDl { get; set; }

    public int? TotalFollowUpNo { get; set; }

    public int? IsNcdFollowUps { get; set; }

    public int? LdlmgDl { get; set; }

    public bool? InProcess { get; set; }

    public bool? IsNcd { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
