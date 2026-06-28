using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class EmrDeviceInformation
{
    public long Id { get; set; }

    public Guid LoggedInUserId { get; set; }

    public long? HealthFacilityId { get; set; }

    public DateTime? DateTimeCreatedAt { get; set; }

    public string? UserDesignation { get; set; }

    public string? DeviceImei { get; set; }

    public string? DeviceModel { get; set; }

    public string? DeviceMake { get; set; }

    public double? DeviceLatitude { get; set; }

    public double? DeviceLongitude { get; set; }
}
