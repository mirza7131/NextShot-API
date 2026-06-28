using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class SampleConsignmentDetail
{
    public Guid SampleConsignmentDetailId { get; set; }

    public Guid? SampleConsignmentId { get; set; }

    public int? LabTestId { get; set; }

    public string? LabTestName { get; set; }

    public Guid? PatientLabTestId { get; set; }

    public string? StatusReason { get; set; }

    public byte? Status { get; set; }

    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }

    public virtual SampleConsignment? SampleConsignment { get; set; }
}
