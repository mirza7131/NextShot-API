using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class UnknownPatient
{
    public Guid UnknownPatientId { get; set; }

    public string? Mrno { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }

    public string? GuardianName { get; set; }

    public string? NameOfCnicHolder { get; set; }

    public bool? IsSelf { get; set; }

    public Guid? RelationProfileId { get; set; }

    public string Cnic { get; set; } = null!;

    public int? Age { get; set; }

    public DateTime? Dob { get; set; }

    public Guid? NationalityProfileId { get; set; }

    public string? PassportNo { get; set; }

    public Guid? MotherLandProfileId { get; set; }

    public Guid? CasteProfileId { get; set; }

    public Guid? GenderProfileId { get; set; }

    public Guid? BloodGroupProfileId { get; set; }

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public string? Ntn { get; set; }

    public bool? IsAlive { get; set; }

    public int? ProvinceId { get; set; }

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int TehsilId { get; set; }

    public int? UnionCouncilId { get; set; }

    public int? HealthFacilityId { get; set; }

    public string? Hfmiscode { get; set; }

    public string? ParmanentAddress { get; set; }

    public string? TemporaryAddress { get; set; }

    public Guid? ReligionProfileId { get; set; }

    public Guid? MaritialStatusProfileId { get; set; }

    public string? Domicile { get; set; }

    public DateTime? FollowupDate { get; set; }

    public bool? IsRegisteredExternally { get; set; }

    public string? SourcePatientId { get; set; }

    public string? SourceMrno { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public long? UserLog { get; set; }

    public byte ActionTypeId { get; set; }

    public Guid? SourceSystemId { get; set; }
}
