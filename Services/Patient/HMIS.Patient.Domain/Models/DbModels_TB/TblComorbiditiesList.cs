using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class TblComorbiditiesList
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? RecordStatus { get; set; }

    public string? CreationBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<TblComorbidity> TblComorbidities { get; } = new List<TblComorbidity>();
}
