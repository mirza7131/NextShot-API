using System;
using System.Collections.Generic;

namespace HMIS.Pathalogy.Domain.Models.DbModels;

public partial class PatientPrescription
{
    public Guid PatientPrescriptionId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? MedicineId { get; set; }

    public Guid? MedicineTypeId { get; set; }

    public int? Days { get; set; }

    public Guid? DoseProfileId { get; set; }

    public Guid? DoseTimeProfileId { get; set; }

    public Guid? PrescribedBy { get; set; }

    public int? Quantity { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public string? MedicineName { get; set; }

    public int? AvailableQuantity { get; set; }

    public decimal? MorningDose { get; set; }

    public decimal? AfterNoonDose { get; set; }

    public decimal? EveningDose { get; set; }

    public decimal? NightDose { get; set; }

    public bool? BeforeFood { get; set; }

    public bool? AfterFood { get; set; }

    public bool? BeforeBreakFast { get; set; }

    public string? MedicineDose { get; set; }

    public string? MedicineRoute { get; set; }

    public string? MedicineFrequency { get; set; }

    public string? MedicineInstruction { get; set; }

    public string? MedicineDuration { get; set; }

    public string? BatchNo { get; set; }

    public Guid? MedicineResource { get; set; }

    public Guid? MedicineResourceProfileId { get; set; }

    public decimal? UnitPrice { get; set; }

    public Guid? MedicineTypeProfileId { get; set; }

    public virtual Profile? DoseProfile { get; set; }

    public virtual Profile? DoseTimeProfile { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual PatientDiagnose? PatientDiagnose { get; set; }

    public virtual PatientOpenVisit? PatientVisit { get; set; }

    public virtual User? PrescribedByNavigation { get; set; }
}
