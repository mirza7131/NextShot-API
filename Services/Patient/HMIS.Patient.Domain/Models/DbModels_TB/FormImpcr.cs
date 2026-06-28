using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class FormImpcr
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

    public bool? Cough { get; set; }

    public bool? Fever { get; set; }

    public bool? WeightLoss { get; set; }

    public bool? NightSweats { get; set; }

    public string? TestConducted { get; set; }

    public string? NameNotifying { get; set; }

    public string? DesignationNotifying { get; set; }

    public string? AddressNotifying { get; set; }

    public string? ContactPersonNo { get; set; }

    public string? EmailReffering { get; set; }

    public DateTime? DateOfExperiencing { get; set; }

    public DateTime? DateOfSendingNotification { get; set; }

    public int? MobCreated { get; set; }

    public string? Created { get; set; }

    public string? Updated { get; set; }

    public int? MobUpdated { get; set; }

    public string? Lat { get; set; }

    public string? Lng { get; set; }

    public int? UserId { get; set; }

    public string? Source { get; set; }

    public bool? SoftDelete { get; set; }
}
