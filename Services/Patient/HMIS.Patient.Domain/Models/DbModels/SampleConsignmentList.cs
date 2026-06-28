using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class SampleConsignmentList
{
    public Guid SampleConsignmentId { get; set; }

    public string? Title { get; set; }

    public int? ToHealthFacility { get; set; }

    public int? FromHealthFacility { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public Guid? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }
}
