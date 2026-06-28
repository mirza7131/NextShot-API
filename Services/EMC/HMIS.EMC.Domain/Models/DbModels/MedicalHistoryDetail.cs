using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class MedicalHistoryDetail
{
    public Guid MedicalHistoryDetailId { get; set; }

    public Guid MedicalHistoryId { get; set; }

    public Guid? PelvicInflammatoryDiseaseProfileId { get; set; }

    public Guid? InvestigationsProfileId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }
}
