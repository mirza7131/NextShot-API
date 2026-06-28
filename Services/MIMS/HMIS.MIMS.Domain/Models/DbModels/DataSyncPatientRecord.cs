using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class DataSyncPatientRecord
{
    public int DataSyncPatientRecordId { get; set; }

    public Guid? OldPatientId { get; set; }

    public Guid? NewPatientId { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
