using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class DeadBodiesListView
{
    public Guid UnknownPatientId { get; set; }

    public string? TransactionId { get; set; }

    public string? Message { get; set; }

    public string? Mrno { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MobileNo { get; set; }

    public int? Age { get; set; }

    public Guid? GenderProfileId { get; set; }

    public int? ProvinceId { get; set; }

    public int? HealthFacilityId { get; set; }

    public bool? IsPendingVerification { get; set; }

    public DateTime? TransctionSaveTime { get; set; }

    public bool? IsTransactionSaveSuccessfully { get; set; }

    public byte ActionTypeId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }
}
