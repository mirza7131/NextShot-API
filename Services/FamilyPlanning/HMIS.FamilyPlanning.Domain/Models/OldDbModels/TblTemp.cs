using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblTemp
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public int? MedicineId { get; set; }

    public int? MedicineDuration { get; set; }

    public int? MedDisburseDate { get; set; }

    public int? FollowupDate { get; set; }

    public string? Mrn { get; set; }
}
