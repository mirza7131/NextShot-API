using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientMedicineAdjustment
{
    public int Id { get; set; }

    public int? ReturnQuantity { get; set; }

    public int? PatientMedicineHistoryId { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletionDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? RecordStatus { get; set; }

    public virtual PatientMedicineHistory? PatientMedicineHistory { get; set; }
}
