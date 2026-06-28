using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class IndentByWard
{
    public Guid IndentByWardId { get; set; }

    public int? HealthfacilityId { get; set; }

    public int? WardId { get; set; }

    public Guid? VendorId { get; set; }

    public Guid? DoctorDesignationProfileId { get; set; }

    public Guid? DoctorUserId { get; set; }

    public string? WardInchargeName { get; set; }

    public string? IndentNumber { get; set; }

    public Guid? LpindentStatusProfileId { get; set; }

    public bool? IsSync { get; set; }

    public bool? IsAcknowledgedByLp { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? IssuedOn { get; set; }

    public Guid? IssuedBy { get; set; }

    public DateTime? ReceivedOn { get; set; }

    public Guid? ReceivedBy { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }
}
