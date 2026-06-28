using System;
using System.Collections.Generic;

namespace HMIS.Data.SyncUtility.Models.DbModels;

public partial class DepartmentLookup
{
    public int DepartmentLookupId { get; set; }

    public string? Name { get; set; }

    public string? DisplayName { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public int? MimsDepartmentId { get; set; }

    public virtual ICollection<HfDepartment> HfDepartments { get; } = new List<HfDepartment>();

    public virtual ICollection<PatientAdmissionDetail> PatientAdmissionDetailDepartmentLookups { get; } = new List<PatientAdmissionDetail>();

    public virtual ICollection<PatientAdmissionDetail> PatientAdmissionDetailShiftedFromDepartmentLookups { get; } = new List<PatientAdmissionDetail>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitDepartementLookups { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitIpdDepartmentLookups { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitIpdReferredByDepartmentLookups { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitReferredDepartmentLookups { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PhysiotherapyModality> PhysiotherapyModalities { get; } = new List<PhysiotherapyModality>();

    public virtual ICollection<SectionLookup> SectionLookups { get; } = new List<SectionLookup>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
