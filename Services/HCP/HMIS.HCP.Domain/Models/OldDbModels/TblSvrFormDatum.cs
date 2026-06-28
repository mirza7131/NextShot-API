using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblSvrFormDatum
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public int? NoOfMonthlyPackPatientReceived { get; set; }

    public int? NoOfMonthlyPackPatientUsed { get; set; }

    public int? NoOfMonthPatTreatmentCompleted { get; set; }

    public string? GapBtwTreatment { get; set; }

    public string? IsEtrPcr { get; set; }

    public string? EtrPcrLabname { get; set; }

    public string? EtrPcrLabnameOther { get; set; }

    public string? IsPatAchieveEtr { get; set; }

    public string? IsSvrPcr { get; set; }

    public string? SvrPcrLabname { get; set; }

    public string? SvrPcrLabnameOther { get; set; }

    public string? IsPatAchieveSvr { get; set; }

    public string? SampleRecommended { get; set; }

    public string? SampleTag { get; set; }

    public string? IsNewRegimePat { get; set; }

    public int? Created { get; set; }

    public int? HospitalId { get; set; }
}
