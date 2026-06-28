using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class PostMortemInternalExamination
{
    public Guid PostMortemInternalExaminationId { get; set; }

    public Guid? MlcpostmortemId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Scalp { get; set; }

    public string? Skull { get; set; }

    public string? Membranes { get; set; }

    public string? Brain { get; set; }

    public string? Vertebrae { get; set; }

    public string? SpinalCord { get; set; }

    public string? WallsStemum { get; set; }

    public string? Pleurae { get; set; }

    public string? LaryNx { get; set; }

    public string? RightLung { get; set; }

    public string? LeftLung { get; set; }

    public string? Pencardium { get; set; }

    public string? BloodVes { get; set; }

    public string? Walls { get; set; }

    public string? Peritoneum { get; set; }

    public string? Mouth { get; set; }

    public string? Diaphragm { get; set; }

    public string? StomachContents { get; set; }

    public string? Pancrease { get; set; }

    public string? SintestineContents { get; set; }

    public string? LintestineContents { get; set; }

    public string? Liver { get; set; }

    public string? Spleen { get; set; }

    public string? Rkidneys { get; set; }

    public string? Lkidneys { get; set; }

    public string? Unnary { get; set; }

    public string? Organs { get; set; }

    public string? Ulinjuries { get; set; }

    public string? Uldisease { get; set; }

    public string? Ulfracutre { get; set; }

    public string? Uldislocation { get; set; }

    public string? Llinjuries { get; set; }

    public string? Lldisease { get; set; }

    public string? Llfracutre { get; set; }

    public string? Lldislocation { get; set; }

    public bool? ChemicalExam { get; set; }

    public bool? Dnalab { get; set; }

    public bool? Histopathologist { get; set; }

    public bool? BallisticExpert { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
