using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class PurposeOfVisit
{
    public Guid PurposeOfVisitId { get; set; }

    public Guid ProstheticId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientOpenVisitId { get; set; }

    public Guid PatientDiagnoseId { get; set; }

    public Guid PurposeOfVisitProfileId { get; set; }

    public int ActionTypeId { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }
}
