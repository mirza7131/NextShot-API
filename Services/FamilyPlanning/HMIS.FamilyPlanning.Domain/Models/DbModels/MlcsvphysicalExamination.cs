using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class MlcsvphysicalExamination
{
    public Guid MlcsvphysicalExaminationId { get; set; }

    public Guid? MlcsvinitialInfoId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? ClothExamination { get; set; }

    public string? TypeOfCloths { get; set; }

    public string? CutsTearsHoles { get; set; }

    public string? BloodStaining { get; set; }

    public string? NonBiologicalMaterialStaining { get; set; }

    public string? GeneralPhysicalExamination { get; set; }

    public string? Physique { get; set; }

    public string? Confident { get; set; }

    public string? Confused { get; set; }

    public string? HeightWeight1 { get; set; }

    public string? CharacteristicsOfInjuries { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual MlcsvinitialInfo? MlcsvinitialInfo { get; set; }
}
