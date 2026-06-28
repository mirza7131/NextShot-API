using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class ViewTblabTest
{
    public int LabTestId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public string? Name { get; set; }

    public string? TestName { get; set; }

    public string? Result { get; set; }
}
