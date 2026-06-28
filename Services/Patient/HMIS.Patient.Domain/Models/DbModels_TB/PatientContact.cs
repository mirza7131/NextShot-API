using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientContact
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public byte? Age { get; set; }

    public string? Gender { get; set; }

    public string? Cnic { get; set; }

    public string? ContactNumber { get; set; }

    public string? Address { get; set; }

    public string? RelationWithPatient { get; set; }

    public bool? IsVerbalScreening { get; set; }

    public string? Montoux { get; set; }

    public string? Igra { get; set; }

    public string? Ssmresult { get; set; }

    public string? GeneXpertResult { get; set; }

    public string? ChestXrayResult { get; set; }

    public string? OtherChestXrayResult { get; set; }

    public string? Diagnosis { get; set; }

    public string? ActionTaken { get; set; }

    public bool? PreventiveTherapyStarted { get; set; }

    public string? Treatment { get; set; }

    public int? PatientId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual ICollection<PatientContactSymptom> PatientContactSymptoms { get; } = new List<PatientContactSymptom>();
}
