using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class GdmpatientDetail
{
    public Guid GdmpatientDetailId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public string? IsPregnancy { get; set; }

    public string? RiskFactors { get; set; }

    public string? IsOnRisk { get; set; }

    public string? PlanningPregnancy { get; set; }

    public string? HealthFacilityCode { get; set; }

    public bool? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public string? PregencyType { get; set; }

    public DateTime? Lmpdate { get; set; }

    public string? Previouspregnancy { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }

    public bool? IsActive { get; set; }
}
