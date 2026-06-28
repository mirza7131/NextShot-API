using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class PatientUpcomingMedicineLog
{
    public Guid PatientUpcomingMedicineLogId { get; set; }

    public Guid? PatientId { get; set; }

    public bool? IsDispensed { get; set; }

    public DateTime? DispensedOn { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
