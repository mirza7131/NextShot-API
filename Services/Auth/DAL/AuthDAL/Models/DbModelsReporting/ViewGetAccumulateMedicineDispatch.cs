using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class ViewGetAccumulateMedicineDispatch
{
    public int MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public int? QuantityDispatched { get; set; }

    public int? WardId { get; set; }

    public string? HealthFacilityCode { get; set; }

    public int? HealthFacilityHrId { get; set; }

    public int? HealthFacilityId { get; set; }

    public bool? Mimsdispatched { get; set; }
}
