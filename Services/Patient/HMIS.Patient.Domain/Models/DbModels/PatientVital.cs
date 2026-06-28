using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class PatientVital
{
    public Guid PatientVitalId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public bool? IsChangeBpsystolic { get; set; }

    public string? Bpsystolic { get; set; }

    public bool? IsChangeBpdiaSystolic { get; set; }
    public string? BpdiaSystolic { get; set; }

    public bool? IsChangePulse { get; set; }
    public string? Pulse { get; set; }

    public bool? IsChangeTemprature { get; set; }
    public string? Temprature { get; set; }

    public bool? IsChangeWeight { get; set; }
    public string? Weight { get; set; }

    public bool? IsChangeHeight { get; set; }
    public string? Height { get; set; }

    public bool? IsChangeResperatoryRate { get; set; }
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

    public bool? IsChangeBmi { get; set; }
    public string? Bmi { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual PatientOpenVisit? PatientVisit { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual User? VitalsCollectedByNavigation { get; set; }
    public decimal? BloodSugar { get; set; }
    public decimal? Waist { get; set; }
    public decimal? Hip { get; set; }
    public decimal? RatioHipToWaist { get; set; }
}
