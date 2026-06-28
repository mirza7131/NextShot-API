using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Smslog
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public string? Status { get; set; }

    public string? SessionId { get; set; }

    public string? MessageTo { get; set; }

    public string? Message { get; set; }

    public string? Event { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsMessageSent { get; set; }

    public string? MessageId { get; set; }

    public string? ErrorId { get; set; }
}
