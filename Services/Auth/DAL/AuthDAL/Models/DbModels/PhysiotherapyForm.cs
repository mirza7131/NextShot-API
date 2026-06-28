using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class PhysiotherapyForm
{
    public Guid PhysiotherapyFormId { get; set; }

    public Guid PatientDiagnoseId { get; set; }

    public string? PresentingComplaint { get; set; }

    public DateTime? ProblemSince { get; set; }

    public string? AnyComorbidity { get; set; }

    public string? DrugHistory { get; set; }

    public string? SignificantExaminationFindings { get; set; }

    public string? TotalDurationOfTreatmentSession { get; set; }

    public bool? DischargeFromPhysicalTherapyTreatment { get; set; }

    public bool? HomeExercisePlan { get; set; }

    public bool? TreatmentAtDepartment { get; set; }

    public string? Prognosis { get; set; }

    public string? ClinicalDiagnosis { get; set; }

    public string? PhysiotherapyDiagnosis { get; set; }

    public string? KeyTreatment { get; set; }

    public string? PlanOfCare { get; set; }

    public string? FrequencyOfExercise { get; set; }

    public string? IntensityOfExercise { get; set; }

    public string? TypeOfExercise { get; set; }

    public Guid? DischargePlanOfCareProfileId { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual Profile? DischargePlanOfCareProfile { get; set; }

    public virtual PatientDiagnose PatientDiagnose { get; set; } = null!;

    public virtual ICollection<PhysiotherapyModality> PhysiotherapyModalities { get; } = new List<PhysiotherapyModality>();
}
