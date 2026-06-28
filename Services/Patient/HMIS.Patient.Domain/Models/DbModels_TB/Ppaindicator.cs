using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Ppaindicator
{
    public int Id { get; set; }

    public string? IndicatorName { get; set; }

    public bool? RecordStatus { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? DeleteBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? OrderKey { get; set; }

    public virtual ICollection<PpaindicatorsOption> PpaindicatorsOptions { get; } = new List<PpaindicatorsOption>();
}
