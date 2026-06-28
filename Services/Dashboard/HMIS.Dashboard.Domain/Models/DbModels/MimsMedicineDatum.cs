using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class MimsMedicineDatum
{
    public Guid MimsMedicineDataId { get; set; }

    public int? MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public int? MedicineTypeId { get; set; }

    public string? MedicineTypeName { get; set; }

    public int? WardId { get; set; }

    public string? WardName { get; set; }

    public decimal? AvailableQuantity { get; set; }

    public decimal? TotalQuantity { get; set; }

    public decimal? TotalDispatchQuantity { get; set; }

    public int? HealthFacilityId { get; set; }

    public decimal? UnitPrice { get; set; }

    public bool? IsSmlmedicine { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
