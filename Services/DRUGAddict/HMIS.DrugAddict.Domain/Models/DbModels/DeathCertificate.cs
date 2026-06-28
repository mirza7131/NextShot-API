using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class DeathCertificate
{
    public Guid DeathCertificateId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public string? TrackingId { get; set; }

    public Guid? DoctorId { get; set; }

    public string? CrmsNo { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? DeceasedPersonNationalityTypeProfileId { get; set; }

    public Guid? DeceasedPersonGenderTypeProfileId { get; set; }

    public Guid? DeceasedPersonReligionTypeProfileId { get; set; }

    public Guid? RelationWithDeceasedProfileId { get; set; }

    public Guid? EntryStatusProfileId { get; set; }

    public string? DeceasedPersonName { get; set; }

    public string? DeceasedPersonCnic { get; set; }

    public DateTime? DeceasedPersonDob { get; set; }

    public string? DeceasedPersonSicknessPeriod { get; set; }

    public DateTime? DeceasedPersonDateAndTimeOfAdmission { get; set; }

    public DateTime? DeceasedPersonDateAndTimeOfDeath { get; set; }

    public DateTime? DeceasedPersonDateOfBurlal { get; set; }

    public string? DeadBodyReceivedBy { get; set; }

    public string? PlaceOfDeath { get; set; }

    public string? CauseOfDeath { get; set; }

    public string? NatureOfDeath { get; set; }

    public string? BuriedAt { get; set; }

    public string? MotherName { get; set; }

    public string? MotherCnic { get; set; }

    public string? FatherName { get; set; }

    public string? FatherCnic { get; set; }

    public string? HusbandName { get; set; }

    public string? HusbandCnic { get; set; }

    public string? Address { get; set; }

    public string? QrCodeImagePath { get; set; }

    public int? TehsilId { get; set; }

    public int? DistrictId { get; set; }

    public string? ApplicantName { get; set; }

    public string? ApplicantCnic { get; set; }

    public DateTime? IssueDate { get; set; }

    public DateTime? EntryDate { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
