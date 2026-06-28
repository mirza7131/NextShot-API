using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class TblComorbidity
{
    public int Id { get; set; }

    public int? TreatmentId { get; set; }

    public int? ComorbidityId { get; set; }

    public string? OtherComorbidity { get; set; }

    public string? RecordStatus { get; set; }

    public string? CreationBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TblComorbiditiesList? Comorbidity { get; set; }

    public virtual PatientTreatmentInfoTb? Treatment { get; set; }
}
