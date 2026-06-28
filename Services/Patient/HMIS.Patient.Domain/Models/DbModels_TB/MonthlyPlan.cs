using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class MonthlyPlan
{
    public int Id { get; set; }

    public string? ActivityMonth { get; set; }

    public DateTime? ActivityDate { get; set; }

    public string? FromDistrict { get; set; }

    public string? ToDistrict { get; set; }

    public int? TypeId { get; set; }

    public int? PlaceId { get; set; }

    public string? Comments { get; set; }

    public string? Hfcode { get; set; }

    public int? ComponentId { get; set; }

    public bool? IsCompleted { get; set; }

    public DateTime? CompletedDate { get; set; }

    public string? CompletedBy { get; set; }

    public string? FocalPerson { get; set; }

    public string? Images { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletionDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public string? Designation { get; set; }

    public virtual ComponentCovered? Component { get; set; }

    public virtual ActivityType? Type { get; set; }
}
