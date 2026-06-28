using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblPatientvaccination
{
    public int Id { get; set; }

    public int Pid { get; set; }

    public int Stage { get; set; }

    public int DoseDate { get; set; }

    public int Created { get; set; }

    public int Updated { get; set; }

    public int UserId { get; set; }

    public int? UserHospital { get; set; }

    public int? UpdatedBy { get; set; }

    public string? EntryType { get; set; }
}
