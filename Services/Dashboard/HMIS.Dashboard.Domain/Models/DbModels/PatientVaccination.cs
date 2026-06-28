using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.DbModels;

public partial class PatientVaccination
{
    public Guid PatientVaccinationId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientDiagnoseId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? VaccinationTypeProfileId { get; set; }

    public Guid? VaccinationProfileId { get; set; }

    public int? VaccinationDoseCount { get; set; }

    public DateTime? VaccinationDose1Date { get; set; }

    public DateTime? VaccinationDose2Date { get; set; }

    public DateTime? VaccinationDose3Date { get; set; }

    public DateTime? VaccinationDose4Date { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
