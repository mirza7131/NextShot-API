using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientTransferLog
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public string? ToDivision { get; set; }

    public string? ToDistrict { get; set; }

    public string? ToTehsil { get; set; }

    public string? ToHf { get; set; }

    public string? FromHf { get; set; }

    public string? Status { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Patient? Patient { get; set; }
}
