using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class GrantApplication
{
    public int Id { get; set; }

    public string? PrincipalName { get; set; }

    public string? Cnic { get; set; }

    public DateTime? Dob { get; set; }

    public DateTime? PrincipalAppointmentDate { get; set; }

    public string? Qualification { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? InstituteName { get; set; }

    public string? OwnerName { get; set; }

    public string? OwnershipProof { get; set; }

    public bool? InstituteOwnedByHead { get; set; }

    public bool? IsLocked { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedAt { get; set; }

    public string? BuildingStatus { get; set; }

    public decimal? BuildingTotalArea { get; set; }

    public decimal? BuildingCoveredArea { get; set; }

    public int? StatusId { get; set; }

    public string? Remarks { get; set; }

    public DateTime? ScrutinyDateTime { get; set; }

    public string? ScrutinyByUserId { get; set; }
}
