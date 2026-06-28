using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class Mlcpostmortem
{
    public Guid MlcpostmortemId { get; set; }

    public Guid? Mlcid { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Pmrno { get; set; }

    public string? FatherHusbandName { get; set; }

    public string? PoliceInformation { get; set; }

    public string? IncidentPlace { get; set; }

    public Guid? McdtypeProfileId { get; set; }

    public Guid? PcdtypeProfileId { get; set; }

    public string? ConsentFile { get; set; }

    public Guid? ImageTypeProfileId { get; set; }

    public Guid? PoliceSignatureTypeProfileId { get; set; }

    public DateTime? PoliceSignatureDateTime { get; set; }

    public string? Radiological { get; set; }

    public string? UltraSound { get; set; }

    public long? ReportCounts { get; set; }

    public DateTime? DeathDateTime { get; set; }

    public DateTime? RecieveDateTime { get; set; }

    public DateTime? DoctorVisitDateTime { get; set; }

    public DateTime? AutopsyDateTime { get; set; }

    public bool? IsFinalReport { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
