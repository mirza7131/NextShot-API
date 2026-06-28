using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientSchedule
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? Month { get; set; }

    public DateTime? LastVisitDate { get; set; }

    public DateTime? NextVisitDate { get; set; }

    public string? LabResults { get; set; }

    public string? Medicine { get; set; }
}
