using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class UnknownPatientsListView
{
    public Guid PatientId { get; set; }

    public string? TransactionId { get; set; }

    public string? Mrno { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MobileNo { get; set; }

    public int? Age { get; set; }

    public Guid? GenderProfileId { get; set; }

    public bool? IsPatientUnknown { get; set; }

    public int? ProvinceId { get; set; }

    public int? HealthFacilityId { get; set; }

    public bool? IsPendingVerification { get; set; }

    public bool? IsTransactionSaveSuccessfully { get; set; }

    public DateTime? TransctionSaveTime { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }
}
