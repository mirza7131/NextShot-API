using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Medicine
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Potency { get; set; }

    public int? ProgramId { get; set; }

    public string? Description { get; set; }

    public string? GenericFormula { get; set; }

    public string? PackingSize { get; set; }

    public string? ManufacturedBy { get; set; }

    public double? Rate { get; set; }

    public string? Remarks { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public int? Uomid { get; set; }

    public virtual ICollection<DistributionPlanDetail> DistributionPlanDetails { get; } = new List<DistributionPlanDetail>();

    public virtual ICollection<PatientMedicineHistory> PatientMedicineHistories { get; } = new List<PatientMedicineHistory>();

    public virtual ICollection<PatientMedicineHistoryLog> PatientMedicineHistoryLogs { get; } = new List<PatientMedicineHistoryLog>();

    public virtual ICollection<StockDetail> StockDetails { get; } = new List<StockDetail>();

    public virtual UnitofMeasurement? Uom { get; set; }
}
