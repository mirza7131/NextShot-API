using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class PatientDiagnosisRecord
{
    public Guid PatientDiagnosisRecordId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientDiagnoseId { get; set; }

    public string? FormType { get; set; }

    public string? Json { get; set; }

    public bool? IsActive { get; set; }
    public bool? IsPrinted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual PatientDiagnose PatientDiagnose { get; set; } = null!;

    public virtual PatientOpenVisit PatientVisit { get; set; } = null!;
}
