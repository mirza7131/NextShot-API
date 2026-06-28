using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientSample
{
    public int Id { get; set; }

    public string? SampleNo { get; set; }

    public string? BarcodeNo { get; set; }

    public string? SpecimenType { get; set; }

    public DateTime? SamplingDate { get; set; }

    public Guid? SamplingBy { get; set; }

    public string? ReceivingStatus { get; set; }

    public string? ReasonForRejection { get; set; }

    public Guid? ReceivedBy { get; set; }

    public DateTime? ReceivingDate { get; set; }

    public string? LabStatus { get; set; }

    public DateTime? LabStatusUpdateDate { get; set; }

    public string? Result { get; set; }

    public Guid? ResultUpdatedBy { get; set; }

    public DateTime? ResultUpdateDate { get; set; }

    public string? VisualAppearance { get; set; }

    public string? SampleDescription { get; set; }

    public string? TestTechnique { get; set; }

    public string? RrValueTb { get; set; }

    public int? ProgramId { get; set; }

    public int? LabId { get; set; }

    public Guid? LabStatusUpdatedBy { get; set; }

    public string? IsoniazidResistance { get; set; }

    public string? FluoroquinoloneResistance { get; set; }

    public string? AmikacinResistance { get; set; }

    public string? KanamycinResistance { get; set; }

    public string? CapreomycinResistance { get; set; }

    public string? EthionamideResistance { get; set; }

    public int? PatientId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public string? SampleReceptionStatus { get; set; }

    public DateTime? SampleRecievingDate { get; set; }

    public DateTime? TestPerformDate { get; set; }

    public string? SampleTestingTechnique { get; set; }

    public string? RefferedBy { get; set; }

    public string? RefferedByOther { get; set; }

    public string? Amikacin10 { get; set; }

    public string? Bedaquiline10 { get; set; }

    public string? Clofazimine10 { get; set; }

    public string? Delamanid06 { get; set; }

    public string? Ethambutol50 { get; set; }

    public string? Isoniazid01 { get; set; }

    public string? Levofloxacin10 { get; set; }

    public string? Linezolid10 { get; set; }

    public string? Moxifloxacin10 { get; set; }

    public string? Pyrazinamide1000 { get; set; }

    public string? Rifampicin05 { get; set; }

    public string? Rifampicin10 { get; set; }

    public string? Streptomycin10 { get; set; }

    public string? GenotypicDrug { get; set; }

    public string? GenotypicGene { get; set; }

    public string? GenotypicPrediction { get; set; }

    public string? TypeOfClinicalCase { get; set; }

    public string? ReasonForTesting { get; set; }

    public Guid? ApprovedBy { get; set; }

    public string? AssayTested { get; set; }

    public string? PrlLpa { get; set; }

    public string? Rifampicin { get; set; }

    public string? Isoniazid { get; set; }

    public string? Fluoroquinolones { get; set; }

    public string? LowLevelKanamycin { get; set; }

    public string? Comments { get; set; }

    public string? Injectables { get; set; }

    public int? VisitId { get; set; }

    public Guid? Guid { get; set; }

    public int? TestId { get; set; }

    public DateTime? PatientReportingDate { get; set; }

    public string? TbCondition { get; set; }

    public int? Month { get; set; }

    public string? SampleTransportMode { get; set; }

    public string? SampleTransportModeDescription { get; set; }

    public DateTime? SampleCollectionDate { get; set; }

    public DateTime? SamplePerformDate { get; set; }

    public string? LabNo { get; set; }

    public string? Grading { get; set; }

    public string? TbbacterialLoad { get; set; }

    public string? RifampicinResistance { get; set; }

    public string? XrayNo { get; set; }

    public string? OtherXray { get; set; }

    public string? Report { get; set; }

    public bool? SuggestForTb { get; set; }

    public string? TestName { get; set; }

    public int? PatientTreatmentProgressId { get; set; }

    public int? Ppascore { get; set; }

    public string? Hfto { get; set; }

    public string? FromHf { get; set; }
}
