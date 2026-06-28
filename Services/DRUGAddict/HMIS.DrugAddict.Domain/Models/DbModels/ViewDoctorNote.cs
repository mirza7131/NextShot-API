using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class ViewDoctorNote
{
    public Guid? PatientvisitId { get; set; }

    public string? Notes { get; set; }

    public DateTime? AdvisedOn { get; set; }

    public string? AdvisedBy { get; set; }
}
