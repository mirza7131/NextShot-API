using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class ViewGetPatientLabTestList
{
    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? LabTestId { get; set; }

    public Guid PatientLabTestId { get; set; }

    public bool? IsRefunded { get; set; }

    public string? Name { get; set; }

    public string? DepartmentName { get; set; }

    public Guid? DepartmentProfileId { get; set; }

    public string? DepartmentShortName { get; set; }

    public Guid? PrescribedBy { get; set; }

    public string? PrescribedByName { get; set; }

    public string? FormType { get; set; }

    public string? PrescribedByDesignation { get; set; }

    public DateTime? PrescribedOn { get; set; }

    public int? TestPrice { get; set; }

    public string Status { get; set; } = null!;

    public bool? IsReportGenerated { get; set; }
}
