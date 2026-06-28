using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class DistributionPlanDetail
{
    public int Id { get; set; }

    public int? DistributionPlanMasterId { get; set; }

    public int? MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public int? Quantity { get; set; }

    public bool? RecordStatus { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual DistributionPlanMaster? DistributionPlanMaster { get; set; }

    public virtual Medicine? Medicine { get; set; }
}
