using System;
using System.Collections.Generic;

namespace HMIS.Data.SyncUtility.Models.DbModels;

public partial class ViewAnmonalList
{
    public Guid? PatientVisitId { get; set; }

    public Guid? PatientId { get; set; }

    public string Cnic { get; set; } = null!;

    public string? MobileNo { get; set; }

    public string? PatientName { get; set; }

    public string? LabDepartmentShortName { get; set; }

    public string LabDepartmentName { get; set; } = null!;

    public bool IsPaid { get; set; }

    public Guid? PaymentReceivedBy { get; set; }

    public DateTime? PaymentReceivedOn { get; set; }

    public DateTime? CreatedOn { get; set; }
}
