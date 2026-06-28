using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class FollowUpType
{
    public int Id { get; set; }

    public string? FollowUpType1 { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletionDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public int? ProgramId { get; set; }

    public virtual ICollection<FollowUpTb> FollowUpTbs { get; } = new List<FollowUpTb>();
}
