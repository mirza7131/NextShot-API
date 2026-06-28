using System;
using System.Collections.Generic;

namespace HMIS.Pathalogy.Domain.Models.DbModels;

public partial class MedicineDispatch
{
    public Guid MedicineDispatchId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public int MedicineId { get; set; }

    public int? QuantityPrescribed { get; set; }

    public int? QuantityDispatch { get; set; }

    public Guid? Pharmacist { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public int? AvailableQuantity { get; set; }

    public string? MedicineName { get; set; }

    public bool? Mimsdispatched { get; set; }

    public string? BatchNo { get; set; }

    public string? Reason { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientPrescriptionId { get; set; }

    public int? WardId { get; set; }

    public Guid? MedicineResource { get; set; }

    public Guid? MedicineResourceProfileId { get; set; }

    public decimal? UnitPrice { get; set; }

    public Guid? MedicineTypeProfileId { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual PatientOpenVisit PatientVisit { get; set; } = null!;

    public virtual User? PharmacistNavigation { get; set; }
}
