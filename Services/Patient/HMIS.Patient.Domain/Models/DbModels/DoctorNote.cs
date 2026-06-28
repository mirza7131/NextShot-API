using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class DoctorNote
{
    public Guid DoctorNotesId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public byte? PatientDepartmentId { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
