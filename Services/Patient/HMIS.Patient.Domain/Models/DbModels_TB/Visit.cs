using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Visit
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? ProgramId { get; set; }

    public string? CurrentStatus { get; set; }

    public string? CurrentStage { get; set; }

    public string? FacilityCode { get; set; }

    public bool? IsTransferred { get; set; }

    public int? TransferredLogId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public int? VisitCount { get; set; }

    public Guid? Guid { get; set; }

    public string? VisitPurpose { get; set; }

    public virtual ICollection<FollowUpTb> FollowUpTbs { get; } = new List<FollowUpTb>();

    public virtual ICollection<PatientMedicineHistory> PatientMedicineHistories { get; } = new List<PatientMedicineHistory>();

    public virtual ICollection<PatientTreatmentInfoTb> PatientTreatmentInfoTbs { get; } = new List<PatientTreatmentInfoTb>();

    public virtual ICollection<PatientVitalsTb> PatientVitalsTbs { get; } = new List<PatientVitalsTb>();
}
