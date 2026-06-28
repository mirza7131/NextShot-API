using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class PsychologicalTestApplied
{
    public Guid PsychologicalTestAppliedId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public bool? IsPsychologicalTestApplied { get; set; }

    public string? BackAnxietyInventory { get; set; }

    public string? BackDepressionInventory { get; set; }

    public int? ConflictResponseScore { get; set; }

    public int? NeutralResponseScore { get; set; }

    public int? PositiveresponseScore { get; set; }

    public string? HouseTreePersonTest { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
