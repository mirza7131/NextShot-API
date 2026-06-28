using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class PatientAdmissionDetailLog
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

    public DateTime ActionDate { get; set; }
}
