using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientMedicineHistory
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? MedicineId { get; set; }

    public string? BatchNo { get; set; }

    public int? QuantityIssued { get; set; }

    public int? FollowUpId { get; set; }

    public int? VisitId { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletionDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public int? Month { get; set; }

    public string? Type { get; set; }

    public int? PatientTreatmentProgressId { get; set; }

    public int? NumberOfDays { get; set; }

    public decimal? PerDayDosage { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? OtherMedicineName { get; set; }

    public virtual FollowUpTb? FollowUp { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<PatientMedicineAdjustment> PatientMedicineAdjustments { get; } = new List<PatientMedicineAdjustment>();

    public virtual PatientTreatmentProgress? PatientTreatmentProgress { get; set; }

    public virtual Visit? Visit { get; set; }
}
