using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.DbModels;

public partial class SourceSystem
{
    public Guid? SourceSystemId { get; set; }

    public string? Name { get; set; }

    public string? ShortName { get; set; }

    public bool? IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual ICollection<PatientLabTest> PatientLabTests { get; } = new List<PatientLabTest>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisits { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<Patient> Patients { get; } = new List<Patient>();
}
