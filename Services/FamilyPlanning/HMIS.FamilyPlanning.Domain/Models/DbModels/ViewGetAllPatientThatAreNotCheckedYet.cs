using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class ViewGetAllPatientThatAreNotCheckedYet
{
    public Guid Mlcid { get; set; }

    public Guid PatientId { get; set; }

    public Guid? DoctorId { get; set; }

    public Guid PatientVisitId { get; set; }

    public string? Mlcno { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? HealthFacilityId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? PatientName { get; set; }

    public string Relation { get; set; } = null!;

    public string? DoctorName { get; set; }

    public string Cnic { get; set; } = null!;

    public string? ShortName { get; set; }
}
