using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class ViewSampleCollectedConsignmentList
{
    public Guid PatientLabTestId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public string? BarcodeNo { get; set; }

    public string? PreGeneratedBarcodeNo { get; set; }

    public int? LabTestId { get; set; }

    public Guid? LabDepartmentProfileId { get; set; }

    public Guid? RiderUserId { get; set; }

    public byte StatusCode { get; set; }

    public string? StatusName { get; set; }

    public string? StageName { get; set; }

    public bool? IsFromCallCenter { get; set; }

    public string? LabDepartmentShortName { get; set; }

    public string LabDepartmentName { get; set; } = null!;

    public string Cnic { get; set; } = null!;

    public string? MrNo { get; set; }

    public string? PatientName { get; set; }

    public string? LabTestName { get; set; }

    public string? AdvisedBy { get; set; }

    public DateTime? AdvisedOn { get; set; }

    public string? PatientMobileNo { get; set; }

    public decimal TestPrice { get; set; }

    public string? LabType { get; set; }

    public string? LabTypeShortName { get; set; }

    public string? ReportLink { get; set; }

    public string? ResultImageLink { get; set; }

    public bool IsSampleRequired { get; set; }

    public bool IsActive { get; set; }

    public bool? IsSampleCollected { get; set; }

    public string? SampleCollectedBy { get; set; }

    public Guid? SampleCollectedById { get; set; }

    public DateTime? SampleCollectedOn { get; set; }

    public bool? IsReportGenerated { get; set; }

    public string? ReportGeneratedBy { get; set; }

    public Guid? ReportGeneratedById { get; set; }

    public DateTime? ReportGeneratedOn { get; set; }

    public bool? IsSampleRejected { get; set; }

    public string? SampleRejectedBy { get; set; }

    public DateTime? SampleRejectedOn { get; set; }

    public string? SampleRejectedReason { get; set; }

    public bool? IsArchived { get; set; }

    public Guid? ArchivedBy { get; set; }

    public DateTime? ArchivedOn { get; set; }

    public bool? IsAdvisedExternally { get; set; }

    public string? SourceDoctorName { get; set; }

    public string? SampleType { get; set; }

    public int ProvinceId { get; set; }

    public int DivisionId { get; set; }

    public int DistrictId { get; set; }

    public int TehsilId { get; set; }

    public int HealthFacilityId { get; set; }

    public string? HealthFacilityName { get; set; }

    public Guid? UserId { get; set; }

    public string? DocDepartmentName { get; set; }

    public string? DocSectionName { get; set; }

    public byte? IsEdit { get; set; }

    public int? ConsignmentToHealthFacilityId { get; set; }

    public Guid SampleConsignmentId { get; set; }

    public string? BatchNumber { get; set; }

    public byte? ConsignmentStatus { get; set; }
}
