using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class ActivitySurvey
{
    public int Id { get; set; }

    public int? IndicatorId { get; set; }

    public DateTime? Answer { get; set; }

    public string? Val { get; set; }

    public string? Remarks { get; set; }

    public int? ScheduleId { get; set; }

    public int? TypeId { get; set; }

    public int? PlaceId { get; set; }

    public string? DistrictCode { get; set; }

    public string? Hfcode { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletionDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public virtual ActivityQuestion? Indicator { get; set; }

    public virtual ActivityType? Type { get; set; }
}
