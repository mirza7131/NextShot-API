using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class Person
{
    public int Id { get; set; }

    public long? EntityId { get; set; }

    public int? PersonTypeId { get; set; }

    public string? NameTitle { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Cnic { get; set; }

    public int? CnicrelationId { get; set; }

    public string? CnicrelationName { get; set; }

    public DateTime? Dob { get; set; }

    public string? Gender { get; set; }

    public string? BloodGroup { get; set; }

    public string? MobileNumber { get; set; }

    public string? MobileNumberSecondary { get; set; }

    public string? Email { get; set; }

    public string? EmailSecondary { get; set; }

    public string? LandlineNumber { get; set; }

    public int? NationalTaxNumber { get; set; }

    public string? PermanentAddress { get; set; }

    public string? CorrespondenceAddress { get; set; }

    public int? ReligionId { get; set; }

    public int? MaritalStatusId { get; set; }

    public int? DomicileId { get; set; }

    public string? Source { get; set; }

    public string? Hfmiscode { get; set; }

    public string? FatherName { get; set; }

    public string? Nationality { get; set; }

    public string? PassportNo { get; set; }

    public string? PlaceOfBirth { get; set; }

    public string? Caste { get; set; }

    public string? PostalCode { get; set; }

    public int? Age { get; set; }

    public string? HealthFacilityName { get; set; }

    public bool IsRegisteredHmis { get; set; }
}
