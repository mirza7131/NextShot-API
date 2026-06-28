using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class ViewIpdpatientVisit
{
    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid PatientDiagnoseId { get; set; }

    public string? FullName { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MobileNo { get; set; }

    public string? Mrno { get; set; }

    public int? Age { get; set; }

    public DateTime? PatientAdmittedOn { get; set; }

    public DateTime? DateOfDischarge { get; set; }

    public Guid? PatientVitalId { get; set; }

    public string? Bpsystolic { get; set; }

    public string? BpdiaSystolic { get; set; }

    public string? Pulse { get; set; }

    public string? Temprature { get; set; }

    public string? Weight { get; set; }

    public string? Height { get; set; }

    public string? ResperatoryRate { get; set; }

    public DateTime? VitalCollectionDate { get; set; }

    public Guid? PatientPrescriptionId { get; set; }

    public string? MedicineName { get; set; }

    public string? MedicineDose { get; set; }

    public string? MedicineFrequency { get; set; }

    public string? MedicineInstruction { get; set; }

    public string? MedicineDuration { get; set; }

    public DateTime? MedicinePrescribeDate { get; set; }

    public int? LabTestId { get; set; }

    public Guid? PatientLabTestId { get; set; }

    public string? LabTestName { get; set; }

    public DateTime? LabTestAdviseDate { get; set; }
}
