using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblScreening
{
    public int Id { get; set; }

    public string? RegNo { get; set; }

    public string? OldRegNo { get; set; }

    public string? MrnNo { get; set; }

    public string? PatientName { get; set; }

    public string? FatherName { get; set; }

    public DateTime? PatientDob { get; set; }

    public float? PatientAge { get; set; }

    public string? PatientType { get; set; }

    public string? RelationContact { get; set; }

    public string? CnicStatus { get; set; }

    public string? SelfCnic { get; set; }

    public string? NextOfKin { get; set; }

    public string? NextOfKinCnic { get; set; }

    public string? NoCnicReason { get; set; }

    public string? ContactNoSelf { get; set; }

    public byte[]? OtherContactno { get; set; }

    public int? Gender { get; set; }

    public string? AddressAvailable { get; set; }

    public string? PostalAddress { get; set; }

    public int? Division { get; set; }

    public int? District { get; set; }

    public int? Tehsil { get; set; }

    public int? Hospital { get; set; }

    public int? Created { get; set; }

    public int? NextStatus { get; set; }

    public int? Updated { get; set; }

    public int? NextStatusUpdated { get; set; }

    public string? PreviousHbv { get; set; }

    public string? PreviousHcv { get; set; }

    public string? PcrConfirmationHbv { get; set; }

    public string? PcrConfirmationHcv { get; set; }

    public string? VacinationCompleted { get; set; }

    public int? MaritalStatus { get; set; }

    public int Occupation { get; set; }

    public int Qualification { get; set; }

    public int? UserId { get; set; }

    public string? CallcenterId { get; set; }

    public string? IsReferal { get; set; }

    public string? CompletedVacinationHbv { get; set; }
}
