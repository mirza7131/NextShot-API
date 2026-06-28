using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class BreastPatientUltraSoundResult
{
    public Guid BreastPatientUltrasoundResultId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public string? LesionSize { get; set; }

    public string? NumberofLesions { get; set; }

    public string? LeftLesion { get; set; }

    public string? RightLesion { get; set; }

    public string? Texture { get; set; }

    public string? Margins { get; set; }

    public string? Orientation { get; set; }

    public string? Shape { get; set; }

    public string? ThinEcogenicCapsule { get; set; }

    public string? PosteriorAcoustic { get; set; }

    public string? GentleLobulation { get; set; }

    public string? MicroCalcification { get; set; }

    public string? ArchitecturalDistortion { get; set; }

    public string? DilatedDucts { get; set; }

    public string? Skinthickening { get; set; }

    public string? LymphNodesEnlarged { get; set; }

    public string? LymphLocation { get; set; }

    public string? LymphNodesSize { get; set; }

    public string? FattyHilum { get; set; }

    public string? CorticalThickness { get; set; }

    public string? RadiologistImpression { get; set; }

    public Guid? BreastCancerPatientDetailId { get; set; }

    public bool Status { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? Fnac { get; set; }

    public string? SampleReceived { get; set; }

    public DateTime? SampleReceivingDate { get; set; }

    public string? SpecimenAdequacy { get; set; }

    public string? SampleResult { get; set; }

    public string? DiagnosisComments { get; set; }

    public DateTime? SampleResultDate { get; set; }

    public string? SampleBarcode { get; set; }

    public bool? IsActive { get; set; }

    public byte? ActionTypeId { get; set; }
}
