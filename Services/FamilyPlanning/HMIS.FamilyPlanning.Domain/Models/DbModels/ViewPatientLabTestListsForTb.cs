using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class ViewPatientLabTestListsForTb
{
    public Guid? PatientVisitId { get; set; }

    public string? LabTestName { get; set; }

    public bool? IsReportGenerated { get; set; }
}
