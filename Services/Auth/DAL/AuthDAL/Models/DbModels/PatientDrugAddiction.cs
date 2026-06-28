using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class PatientDrugAddiction
{
    public Guid PatientDrugAddictionId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? AddictionProfileId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte? ActionTypeId { get; set; }
}
