using System;
using System.Collections.Generic;

namespace HMIS.Data.SyncUtility.Models.DbModels;

public partial class RiskFactor
{
    public Guid RiskFactorId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? RiskFactorProfileId { get; set; }

    public string? Answer { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }
}
