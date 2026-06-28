using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class FormOltp
{
    public int Id { get; set; }

    public string? NameOfPatient { get; set; }

    public string? NameOfFatherHusband { get; set; }

    public DateTime? Dob { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Cnic { get; set; }

    public string? MicroResultOfSputum { get; set; }

    public string? MicroResultOfFluid { get; set; }

    public string? CultureOfSputum { get; set; }

    public string? CultureOfBodyFluid { get; set; }

    public bool? GXpert { get; set; }

    public string? NameOfIncharge { get; set; }

    public string? Designation { get; set; }

    public string? Qualification { get; set; }

    public string? MedicineInUse { get; set; }

    public string? TestAddress { get; set; }

    public string? ContactPersonNo { get; set; }

    public string? Email { get; set; }

    public string? TestPersonName { get; set; }

    public DateTime? DateOfSpecimen { get; set; }

    public string? TestPersonDesignation { get; set; }

    public DateTime? DateOfSendingNotification { get; set; }

    public int? MobCreated { get; set; }

    public string? Created { get; set; }

    public string? Updated { get; set; }

    public Guid? UpdatedBy { get; set; }

    public string? Status { get; set; }

    public int? MobUpdated { get; set; }

    public string? Lat { get; set; }

    public string? Lng { get; set; }

    public int? UserId { get; set; }

    public string? Source { get; set; }
}
