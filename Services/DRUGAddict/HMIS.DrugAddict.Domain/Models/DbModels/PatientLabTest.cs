using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

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

    public string? SourceBarcode { get; set; }

    public string? SourceDoctorName { get; set; }

    public Guid? SampleConsignmentDetailId { get; set; }

    public DateTime? BatchCreatedOn { get; set; }

    public Guid? BatchCreatedBy { get; set; }

    public DateTime? BatchResultUploadedOn { get; set; }

    public Guid? BatchResultUploadedBy { get; set; }

    public string? BatchNumber { get; set; }

    public bool? IsScannedBatch { get; set; }

    public Guid? LabSampleBatchId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public string? SourcePkId { get; set; }

    public bool? IsExternalSampleUpdated { get; set; }

    public bool? IsExternalReportUpdated { get; set; }

    public string? ReportLink { get; set; }

    public bool? IsSampleRequired { get; set; }

    public string? PreGeneratedBarcodeNo { get; set; }

    public string? ResultImageLink { get; set; }

    public bool? IsFromCallCenter { get; set; }

    public Guid? SourceSystemId { get; set; }

    public Guid? RiderUserId { get; set; }

    public Guid? StatusUpdatedBy { get; set; }

    public DateTime? StatusUpdatedOn { get; set; }

    public byte? IsEdit { get; set; }

    public bool? IsConsignementLabTestReportApproved { get; set; }

    public string? ConsignmentLabTestReportRejectedReason { get; set; }

    public Guid? ConsignmentLabTestStatusUpdatedBy { get; set; }

    public DateTime? ConsignmentLabTestStatusUpdatedOn { get; set; }

    public bool? IsOnBedSample { get; set; }

    public bool? IsPaid { get; set; }

    public Guid? PaymentReceivedBy { get; set; }

    public DateTime? PaymentReceivedOn { get; set; }

    public decimal? TestPrice { get; set; }

    public decimal? DiscountInPercentage { get; set; }

    public decimal? DiscountedPrice { get; set; }

    public Guid? DiscountedByProfileId { get; set; }

    public bool? IsRefunded { get; set; }

    public string? RefundReason { get; set; }

    public Guid? PaymentRefundBy { get; set; }

    public DateTime? PaymentRefundOn { get; set; }

    public string? LabNo { get; set; }

    public virtual Profile? LabDepartmentProfile { get; set; }

    public virtual LabTest? LabTest { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual User? ReportGeneratedByNavigation { get; set; }

    public virtual User? SampleCollectedByNavigation { get; set; }

    public virtual User? SampleRejectedByNavigation { get; set; }

    public virtual User? TestAdvisedByNavigation { get; set; }
}
