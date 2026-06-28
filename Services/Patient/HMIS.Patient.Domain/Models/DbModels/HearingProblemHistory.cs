using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class HearingProblemHistory
{
    public Guid? HearingProblemHistoryId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public bool? IsHearingLoss { get; set; }

    public string? TypeOfHearingLoss { get; set; }

    public string? NatureOfHearingLoss { get; set; }

    public string? LevelOfHearingLoss { get; set; }

    public string? UseOfHearingAid { get; set; }

    public int? AgeOfWearingHearingAid { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }
    public int? ActionTypeId { get; set; }
}