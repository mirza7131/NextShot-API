using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class PatientWorkFlowLog
{
    public Guid PatientWorkFlowLogId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? CurrentStationProfileId { get; set; }

    public Guid? NextStationProfileId { get; set; }

    public bool? IsVisitClose { get; set; }

    public string? TimeDifference { get; set; }

    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
