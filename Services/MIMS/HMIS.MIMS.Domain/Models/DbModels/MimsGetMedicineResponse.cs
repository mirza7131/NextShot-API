using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class MimsGetMedicineResponse
{
    public Guid MimsGetMedicineResponseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? ResponseData { get; set; }

    public int? WardId { get; set; }

    public string? WardName { get; set; }

    public long? MimsIndentId { get; set; }

    public bool? SyncStatus { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public byte? ActionTypeId { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }
}
