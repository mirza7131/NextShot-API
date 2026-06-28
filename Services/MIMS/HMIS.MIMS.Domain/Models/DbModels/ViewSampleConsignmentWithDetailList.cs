using System;
using System.Collections.Generic;

namespace HMIS.MIMS.Domain.Models.DbModels;

public partial class ViewSampleConsignmentWithDetailList
{
    public Guid SampleConsignmentId { get; set; }

    public string? Title { get; set; }

    public int FromHealthFacilityId { get; set; }

    public int ToHealthFacilityId { get; set; }

    public string? FromHealthFacility { get; set; }

    public string? ToHealthFacility { get; set; }

    public string? ConsignmentStatusReason { get; set; }

    public byte? ConsignmentStatus { get; set; }

    public DateTime? SampleConsignmentCreatedOn { get; set; }

    public string? BatchNo { get; set; }

    public string? LabTestName { get; set; }

    public Guid? PatientLabTestId { get; set; }

    public int? LabTestId { get; set; }

    public string? BarcodeNo { get; set; }

    public Guid SampleConsignmentDetailId { get; set; }

    public string? PatientName { get; set; }

    public byte? ConsignmentDetailStatus { get; set; }

    public string? ConsignmentDetailStatusReason { get; set; }

    public Guid? SampleConsignmentCreatedbyId { get; set; }

    public string? SampleConsignmentCreatedBy { get; set; }

    public Guid? SampleConsignmentDetailCreatedbyId { get; set; }

    public string? SampleConsignmentDetailCreatedBy { get; set; }

    public DateTime? SampleConsignemntCreated { get; set; }

    public DateTime? SampleConsignemntDetailCreated { get; set; }

    public string? PreGeneratedBarcodeNo { get; set; }
}
