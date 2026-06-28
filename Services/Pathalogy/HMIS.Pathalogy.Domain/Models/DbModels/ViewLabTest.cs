using System;
using System.Collections.Generic;

namespace HMIS.Pathalogy.Domain.Models.DbModels;

public partial class ViewLabTest
{
    public int LabTestId { get; set; }

    public string? Name { get; set; }

    public string? ShortName { get; set; }

    public Guid? DepartmentProfileId { get; set; }

    public Guid? LabTestCategoryProfileId { get; set; }

    public Guid? LabTestTypeProfileId { get; set; }

    public string? SampleType { get; set; }

    public decimal? TestPrice { get; set; }

    public decimal? DoctorShare { get; set; }

    public decimal? GovtShare { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public bool IsActive { get; set; }
}
