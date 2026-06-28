using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class ActivityQuestion
{
    public int Id { get; set; }

    public string? Indicator { get; set; }

    public int? Type { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletionDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public virtual ICollection<ActivitySurvey> ActivitySurveys { get; } = new List<ActivitySurvey>();
}
