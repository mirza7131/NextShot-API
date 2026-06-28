using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class GetAllMlcQueDatum
{
    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public string? TokenNo { get; set; }

    public string? Mrno { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MobileNo { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }

    public int DepartmentLookupId { get; set; }

    public string? DepartmentName { get; set; }

    public int? SectionLookupId { get; set; }

    public string? SectionName { get; set; }

    public string? PatientCondition { get; set; }

    public int? BedNo { get; set; }

    public string? VisitFor { get; set; }

    public DateTime? VisitDate { get; set; }

    public Guid? DoctorId { get; set; }

    public int? HealthFacilityId { get; set; }

    public long? Rn { get; set; }
}
