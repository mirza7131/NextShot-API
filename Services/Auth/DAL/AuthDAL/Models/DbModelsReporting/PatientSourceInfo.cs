using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class PatientSourceInfo
{
    public Guid PatientSourceInfoId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? UnknownPatientId { get; set; }

    public Guid? PatientSourceProfileId { get; set; }

    public string? VehicleNo { get; set; }

    public string? Designation { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeleteBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }

    public string? Name { get; set; }

    public string? ContactNo { get; set; }
}
