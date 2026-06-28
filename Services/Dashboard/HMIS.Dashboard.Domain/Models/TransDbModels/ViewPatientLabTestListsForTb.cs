using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class ViewPatientLabTestListsForTb
{
    public Guid? PatientVisitId { get; set; }

    public string? LabTestName { get; set; }

    public bool? IsReportGenerated { get; set; }
}
