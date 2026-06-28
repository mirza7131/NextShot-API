using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class HwdOldDatum
{
    public int Id { get; set; }

    public string? PhcpUniqueKey { get; set; }

    public string? OldRegistrationNumber { get; set; }

    public string? Name { get; set; }

    public string? FatherName { get; set; }

    public string? HospitalName { get; set; }

    public string? District { get; set; }

    public string? Gender { get; set; }

    public string? Cnic { get; set; }

    public string? Address { get; set; }

    public string? ContactNo { get; set; }

    public string? MaritalStatus { get; set; }

    public DateTime? TestDate { get; set; }

    public double? HbvStatus { get; set; }

    public double? HcvStatus { get; set; }

    public double? VdaStatus { get; set; }

    public double? CmStatus { get; set; }

    public double? VcStatus { get; set; }

    public double? PcrRecommended { get; set; }

    public string? ScSampleRn { get; set; }

    public DateTime? FollowDate { get; set; }

    public string? PcrSentTo { get; set; }

    public string? NextDoseHospital { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? GuardianCnic { get; set; }

    public string? PcrConfirmHbv { get; set; }

    public string? PcrConfirmHcv { get; set; }
}
