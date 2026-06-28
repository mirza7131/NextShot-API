using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class ViewSampleConsignmentList
{
    public Guid SampleConsignmentId { get; set; }

    public string? Title { get; set; }

    public int FromHealthFacilityId { get; set; }

    public int ToHealthFacilityId { get; set; }

    public string? FromHealthFacility { get; set; }

    public string? ToHealthFacility { get; set; }

    public string? ConsignmentStatusReason { get; set; }

    public byte? ConsignmentStatus { get; set; }

    public DateTime? SampleConsignmentCreatedOn { get; set; }

    public string? BatchNo { get; set; }
}
