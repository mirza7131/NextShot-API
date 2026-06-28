using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientTreatmentProgress
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? Month { get; set; }

    public bool? ShowIssueMedicine { get; set; }

    public bool? ShowAdjustment { get; set; }

    public bool? ShowAddResult { get; set; }

    public bool? IsIssueMedicine { get; set; }

    public bool? IsAddAdjustment { get; set; }

    public bool? IsAddResult { get; set; }

    public bool? IsOutcome { get; set; }

    public bool? ShowOutcome { get; set; }

    public bool? RecordStatus { get; set; }

    public bool? IsLocked { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<PatientMedicineHistory> PatientMedicineHistories { get; } = new List<PatientMedicineHistory>();
}
