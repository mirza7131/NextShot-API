using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class IndentMasterLog
{
    public Guid IndentMasterLogId { get; set; }

    public Guid? IndentMasterId { get; set; }

    public string? IndentNumber { get; set; }

    public Guid? FromMimsBranchId { get; set; }

    public Guid? ToMimsBranchId { get; set; }

    public bool? IsMimsAcknowledged { get; set; }

    public Guid? MimsIndentStatusProfileId { get; set; }

    public Guid? MedicineSourceTypeProfileId { get; set; }

    public int? HealthfacilityId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? IssuedOn { get; set; }

    public Guid? IssuedBy { get; set; }

    public DateTime? ReceivedOn { get; set; }

    public Guid? ReceivedBy { get; set; }

    public string? ActionPhrase { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }
}
