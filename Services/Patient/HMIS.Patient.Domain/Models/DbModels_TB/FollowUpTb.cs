using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class FollowUpTb
{
    public int Id { get; set; }

    public int? ProgramId { get; set; }

    public int? PatientId { get; set; }

    public int? VisitId { get; set; }

    public string? Purpose { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public DateTime? PatientReportingDate { get; set; }

    public int? FollowUpTypeId { get; set; }

    public virtual FollowUpType? FollowUpType { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<PatientMedicineHistory> PatientMedicineHistories { get; } = new List<PatientMedicineHistory>();

    public virtual ICollection<PatientMedicineHistoryLog> PatientMedicineHistoryLogs { get; } = new List<PatientMedicineHistoryLog>();

    public virtual Program? Program { get; set; }

    public virtual Visit? Visit { get; set; }
}
