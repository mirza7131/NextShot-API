using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientTreatmentInfoTb
{
    public int Id { get; set; }

    public string? TbType { get; set; }

    public string? EpsiteOfDisease { get; set; }

    public string? OtherSiteOfDisease { get; set; }

    public decimal? Weight { get; set; }

    public int? HouseHoldContacts { get; set; }

    public int? ContactsUnderFive { get; set; }

    public DateTime? TreatmentStartDate { get; set; }

    public string? PatientType { get; set; }

    public int? VisitId { get; set; }

    public int? PatientId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public DateTime? PatientReportingDate { get; set; }

    public string? ReferredBy { get; set; }

    public decimal? BloodPressureSystolic { get; set; }

    public decimal? BloodPressureDiastolic { get; set; }

    public decimal? Temperature { get; set; }

    public decimal? RespiratoryRate { get; set; }

    public bool? IsDiagnosticInfo { get; set; }

    public string? TbregistrationNo { get; set; }

    public bool? IsXray { get; set; }

    public string? Xrayvalue { get; set; }

    public bool? IsSputum { get; set; }

    public string? MicroscopyValue { get; set; }

    public bool? IsGeneXpert { get; set; }

    public string? Tbcondition { get; set; }

    public string? EprelevantInvestigation { get; set; }

    public string? Epresult { get; set; }

    public bool? IsDiscarded { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<TblComorbidity> TblComorbidities { get; } = new List<TblComorbidity>();

    public virtual Visit? Visit { get; set; }
}
