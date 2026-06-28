using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class ViewGetPatientPrescriptionList
{
    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public int? MedicineId { get; set; }

    public string? MedicineName { get; set; }

    public int? Days { get; set; }

    public int? Quantity { get; set; }

    public string? MedicineFrequency { get; set; }

    public string? MedicineDose { get; set; }

    public Guid? PrescribedBy { get; set; }

    public string? PrescribedByName { get; set; }

    public string? FormType { get; set; }

    public string? PrescribedByDesignation { get; set; }

    public DateTime? PrescribedOn { get; set; }

    public bool? IsDispatch { get; set; }
}
