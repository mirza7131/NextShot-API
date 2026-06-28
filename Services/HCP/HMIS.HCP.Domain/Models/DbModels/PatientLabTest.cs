using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.DbModels;

public partial class PatientLabTest
{
    public Guid PatientLabTestId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? LabDepartmentProfileId { get; set; }

    public int? LabTestId { get; set; }

    public string? BarcodeNo { get; set; }

    public byte Status { get; set; }

    public string? SampleTransportMode { get; set; }

    public bool? IsSampleCollected { get; set; }

    public Guid? SampleCollectedBy { get; set; }

    public DateTime? SampleCollectedOn { get; set; }

    public bool? IsReportGenerated { get; set; }

    public Guid? ReportGeneratedBy { get; set; }

    public DateTime? ReportGeneratedOn { get; set; }

    public bool? IsSampleRejected { get; set; }

    public string? SampleRejectedReason { get; set; }

    public Guid? SampleRejectedBy { get; set; }

    public DateTime? SampleRejectedOn { get; set; }

    public bool? IsArchived { get; set; }

    public Guid? ArchivedBy { get; set; }

    public DateTime? ArchivedOn { get; set; }

    public Guid? TestAdvisedBy { get; set; }

    public bool? IsAdvisedExternally { get; set; }

    public string? SourceLabTestId { get; set; }

    public Guid? SourceSystemId { get; set; }

    public string? SourceBarcode { get; set; }

    public string? SourceDoctorName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual Profile? LabDepartmentProfile { get; set; }

    public virtual LabTest? LabTest { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual PatientDiagnose? PatientDiagnose { get; set; }

    public virtual ICollection<PatientLabTestDetail> PatientLabTestDetails { get; } = new List<PatientLabTestDetail>();

    public virtual PatientOpenVisit? PatientVisit { get; set; }

    public virtual User? ReportGeneratedByNavigation { get; set; }

    public virtual User? SampleCollectedByNavigation { get; set; }

    public virtual User? SampleRejectedByNavigation { get; set; }

    public virtual SourceSystem? SourceSystem { get; set; }

    public virtual User? TestAdvisedByNavigation { get; set; }
}
