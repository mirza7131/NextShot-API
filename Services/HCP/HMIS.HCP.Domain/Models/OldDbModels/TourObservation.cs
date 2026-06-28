using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TourObservation
{
    public int Id { get; set; }

    public int ObservationId { get; set; }

    public string Observation { get; set; } = null!;

    public int? ObservationCondition { get; set; }

    public string? ObservationRemarks { get; set; }

    public string? ObservationCompliance { get; set; }

    public int? ComplianceBy { get; set; }

    public DateTime? ComplianceDate { get; set; }

    public int MonitoringAreaId { get; set; }

    public int SubAreaId { get; set; }

    public int TourId { get; set; }

    public int FacilityId { get; set; }

    public int MemberId { get; set; }

    public int? Sequence { get; set; }

    public int? OrderBy { get; set; }

    public int? IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }
}
