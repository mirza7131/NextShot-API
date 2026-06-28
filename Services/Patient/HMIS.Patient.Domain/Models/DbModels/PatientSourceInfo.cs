using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class PatientSourceInfo
{
    public Guid PatientSourceInfoId { get; set; }

    public Guid? PatientId { get; set; }
    public Guid? PatientVisitId { get; set; }

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
    public string? CNIC { get; set; }
    public string? HealthFacility { get; set; }

    public string? LHSName { get; set; }

    public string? LHSContactNo { get; set; }
    public string? LHSCNIC { get; set; }
}
