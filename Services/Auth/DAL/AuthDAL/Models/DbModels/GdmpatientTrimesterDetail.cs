using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class GdmpatientTrimesterDetail
{
    public Guid GdmpatientTrimesterDetailId { get; set; }

    public DateTime? Lmpdate { get; set; }

    public float? Bsr { get; set; }

    public float? Bsf { get; set; }

    public float? HbA1c { get; set; }

    public string? PreGestationalDiabetes { get; set; }

    public string? Previouspregnancy { get; set; }

    public string? Trimester { get; set; }

    public int? TrimesterCounter { get; set; }

    public string? HealthFacilityCode { get; set; }

    public Guid? GdmpatientDetailId { get; set; }

    public bool? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public float? OgttoneHour { get; set; }

    public float? OgtttwoHour { get; set; }

    public string? ConfirmedonTwodifferentdates { get; set; }

    public string? GestationalOgttat16Weeks { get; set; }

    public string? GestationalOgttat24Weeks { get; set; }

    public string? Gestationalagegreaterthan34weeks { get; set; }

    public float? Onehourpostmeal { get; set; }

    public string? PregencyType { get; set; }

    public string? ReferForOgtt { get; set; }

    public string? OgttvalueType { get; set; }

    public int? WeeksDifference { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }

    public bool? IsActive { get; set; }
}
