using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class InstituteDetail
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public int? InstituteTypeId { get; set; }

    public string? InstituteType { get; set; }

    public int? AffliationTypeId { get; set; }

    public string? AffliationType { get; set; }

    public string? UniversityName { get; set; }

    public DateTime? EstablishmentYear { get; set; }

    public int? LegalStatusId { get; set; }

    public string? LegalStatus { get; set; }

    public string? CurrentAffliationUniversity { get; set; }

    public string? AffliatedNumber { get; set; }

    public DateTime? AffliationDate { get; set; }

    public DateTime? AffliationExpiry { get; set; }

    public string? AffliationCertificate { get; set; }

    public string? LegalRegistrationCertificate { get; set; }

    public string? Hospital { get; set; }

    public DateTime? HospitalAffliationDate { get; set; }

    public DateTime? HospitalAffliationExpiry { get; set; }

    public string? HospitalMou { get; set; }

    public int? HospitalSectorId { get; set; }

    public string? HospitalSector { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }
}
