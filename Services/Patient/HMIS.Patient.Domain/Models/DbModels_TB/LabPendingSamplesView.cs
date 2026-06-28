using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class LabPendingSamplesView
{
    public string? LabNo { get; set; }

    public DateTime? SamplingDate { get; set; }

    public string? SampleTransportMode { get; set; }

    public string? SampleTransportModeDescription { get; set; }

    public string? FullName { get; set; }

    public string? HftypeName { get; set; }

    public string? Hfto { get; set; }

    public string? FromHf { get; set; }

    public int SampleId { get; set; }

    public Guid? ReceivedBy { get; set; }

    public string? ReceivingStatus { get; set; }

    public string? SampleNo { get; set; }

    public string? SpecimenType { get; set; }

    public string? BarcodeNo { get; set; }

    public Guid? SamplingBy { get; set; }

    public DateTime? ReceivingDate { get; set; }

    public string? LabStatus { get; set; }

    public DateTime? LabStatusUpdateDate { get; set; }

    public string? Result { get; set; }

    public Guid? ResultUpdatedBy { get; set; }

    public DateTime? ResultUpdateDate { get; set; }

    public DateTime? SampleCollectionDate { get; set; }

    public DateTime? SamplePerformDate { get; set; }

    public string? ReasonForRejection { get; set; }

    public string? HftoFullName { get; set; }
}
