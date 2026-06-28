using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class PatientVital
{
    public Guid PatientVitalId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public string? Bpsystolic { get; set; }

    public string? BpdiaSystolic { get; set; }

    public string? Pulse { get; set; }

    public string? Temprature { get; set; }

    public string? Weight { get; set; }

    public string? Height { get; set; }

    public string? ResperatoryRate { get; set; }

    public Guid? VitalsCollectedBy { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public string? Bmi { get; set; }

    public string? JsonRecord { get; set; }

    public bool? IsChangeBpsystolic { get; set; }

    public bool? IsChangeBpdiaSystolic { get; set; }

    public bool? IsChangePulse { get; set; }

    public bool? IsChangeTemprature { get; set; }

    public bool? IsChangeWeight { get; set; }

    public bool? IsChangeHeight { get; set; }

    public bool? IsChangeResperatoryRate { get; set; }

    public bool? IsChangeBmi { get; set; }
}
