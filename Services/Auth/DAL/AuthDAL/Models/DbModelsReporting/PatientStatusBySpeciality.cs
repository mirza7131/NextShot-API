using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class PatientStatusBySpeciality
{
    public Guid PatientStatusBySpecialityId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public int? DepartmentLookupId { get; set; }

    public int? SectionLookupId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public string? DeseaseSubType { get; set; }

    public string? TbPatientConfirmationType { get; set; }

    public string? FormType { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
