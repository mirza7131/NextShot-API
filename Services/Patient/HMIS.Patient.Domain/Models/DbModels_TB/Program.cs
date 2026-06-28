using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Program
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public virtual ICollection<FollowUpTb> FollowUpTbs { get; } = new List<FollowUpTb>();

    public virtual ICollection<PatientBackup6> PatientBackup6s { get; } = new List<PatientBackup6>();

    public virtual ICollection<ProgramCenter> ProgramCenters { get; } = new List<ProgramCenter>();

    public virtual ICollection<TransferHistory> TransferHistories { get; } = new List<TransferHistory>();
}
