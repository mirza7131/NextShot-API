using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class TempMedicinePricing
{
    public Guid TempMedicinePricingId { get; set; }

    public int? MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public decimal? PricePerItem { get; set; }

    public bool? RecordStatus { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }
}
