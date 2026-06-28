using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class SpeechMilestone
{
    public Guid SpeechMilestoneId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? Cooing { get; set; }

    public int? Babbling { get; set; }

    public int? SingleWord { get; set; }

    public int? PresentSpeechLevel { get; set; }

    public string? SpeechMilestoneStatus { get; set; }

    public bool? IsNoSpeechMilestone { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
