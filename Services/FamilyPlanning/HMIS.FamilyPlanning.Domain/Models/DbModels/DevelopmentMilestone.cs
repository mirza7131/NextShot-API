using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class DevelopmentMilestone
{
    public Guid DevelopmentMilestoneId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? NeckHolding { get; set; }

    public int? Siting { get; set; }

    public int? Standing { get; set; }

    public int? Walking { get; set; }

    public string? DevelopmentMilestoneStatus { get; set; }

    public bool? IsNoDevelopmentMilestone { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
