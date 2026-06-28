using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.DbModels;

public partial class PatientAdmissionDetail
{
    public Guid PatientAdmissionDetailId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? ShiftedFromDepartmentLookupId { get; set; }

    public int? ShiftedFromSectionLookupId { get; set; }

    public int? DepartmentLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public string? ReasonOfShifting { get; set; }

    public string? ShiftedBy { get; set; }

    public DateTime? ShiftedOn { get; set; }

    public string? AdmittedBy { get; set; }

    public DateTime? AdmittedInSpeciality { get; set; }

    public Guid? PatientLevelProfileId { get; set; }

    public bool? IsVantilated { get; set; }

    public DateTime? PatientAdmittedOn { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual DepartmentLookup? DepartmentLookup { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual PatientDiagnose? PatientDiagnose { get; set; }

    public virtual Profile? PatientLevelProfile { get; set; }

    public virtual Profile? PatientStatusProfile { get; set; }

    public virtual PatientOpenVisit PatientVisit { get; set; } = null!;

    public virtual SectionLookup? SectionLookup { get; set; }

    public virtual DepartmentLookup? ShiftedFromDepartmentLookup { get; set; }

    public virtual SectionLookup? ShiftedFromSectionLookup { get; set; }
}
