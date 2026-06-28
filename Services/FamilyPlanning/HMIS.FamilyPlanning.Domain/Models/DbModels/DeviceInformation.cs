using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class DeviceInformation
{
    public Guid UserDeviceInfoId { get; set; }

    public Guid LoggedInUserId { get; set; }

    public long? HealthFacilityId { get; set; }

    public string? UserDesignation { get; set; }

    public string? DeviceImei { get; set; }

    public string? DeviceModel { get; set; }

    public string? DeviceMake { get; set; }

    public double? DeviceLatitude { get; set; }

    public double? DeviceLongitude { get; set; }

    public DateTime? DateTimeCreatedAt { get; set; }

    public Guid? UserIdCreatedBy { get; set; }

    public DateTime? DateTimeUpdatedAt { get; set; }

    public Guid? UserIdUpdatedBy { get; set; }

    public DateTime? DateTimeDeletedAt { get; set; }

    public Guid? UserIdDeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
