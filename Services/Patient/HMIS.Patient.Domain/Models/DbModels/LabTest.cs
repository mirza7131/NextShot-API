using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class LabTest
{
    public int LabTestId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid? DepartmentProfileId { get; set; }

    public Guid? LabTestCategoryProfileId { get; set; }

    public Guid? LabTestTypeProfileId { get; set; }

    public bool? IsSampleRequired { get; set; }

    public string? SampleType { get; set; }

    public decimal? TestPrice { get; set; }

    public decimal? DoctorShare { get; set; }

    public decimal? StaffShare { get; set; }

    public decimal? GovtShare { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual Profile? DepartmentProfile { get; set; }

    public virtual Profile? LabTestCategoryProfile { get; set; }

    public virtual ICollection<LabTestDetail> LabTestDetails { get; } = new List<LabTestDetail>();

    public virtual Profile? LabTestTypeProfile { get; set; }

    public virtual ICollection<PatientLabTest> PatientLabTests { get; } = new List<PatientLabTest>();
}