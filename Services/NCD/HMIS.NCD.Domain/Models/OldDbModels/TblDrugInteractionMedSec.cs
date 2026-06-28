using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblDrugInteractionMedSec
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public string? SampleId { get; set; }

    public string? SampleSvrFlag { get; set; }

    public string? DrugInteraction { get; set; }

    public string? Enticavir { get; set; }

    public string? Tenofovir { get; set; }

    public string? Telbuvidine { get; set; }

    public string? Disburse3MnthDose { get; set; }

    public string? Disburse6MnthDose { get; set; }

    public string? BaselineType { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public string? SdPack { get; set; }

    public string? SrPack { get; set; }

    public string? SdrPack { get; set; }

    public string? IsDemote { get; set; }

    public string? IsImportedData { get; set; }

    public int? MedicineId { get; set; }

    public string? SampleRecommended { get; set; }

    public string? IsAdminFollowUp { get; set; }

    public int? AdminFollowUpDate { get; set; }

    public int? AdminUser { get; set; }

    public int? HospitalId { get; set; }

    public string IsTerminate { get; set; } = null!;

    public string IsDuplicate { get; set; } = null!;
}
