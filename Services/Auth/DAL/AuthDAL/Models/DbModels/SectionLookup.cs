using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class SectionLookup
{
    public int SectionLookupId { get; set; }

    public int DepartmentLookupId { get; set; }

    public string? FormType { get; set; }

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

    public bool? IsConsultant { get; set; }

    public bool? IsFilterClinic { get; set; }

    public int? ConsultantSectionLookupId { get; set; }

    public int? MimsWardId { get; set; }

    public int? HrWardId { get; set; }

    public bool? IsFreeLabTest { get; set; }

    public bool? IsSkipAlmoner { get; set; }

    public byte? SpecialityRunningMode { get; set; }

    public int? SpecialityFee { get; set; }

    public virtual DepartmentLookup DepartmentLookup { get; set; } = null!;

    public virtual ICollection<HfDepartmentSection> HfDepartmentSections { get; } = new List<HfDepartmentSection>();

    public virtual ICollection<PatientAdmissionDetail> PatientAdmissionDetailSectionLookups { get; } = new List<PatientAdmissionDetail>();

    public virtual ICollection<PatientAdmissionDetail> PatientAdmissionDetailShiftedFromSectionLookups { get; } = new List<PatientAdmissionDetail>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitIpdReferredBySectionLookups { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitIpdSectionLookups { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitReferredSectionLookups { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<PatientOpenVisit> PatientOpenVisitSectionLookups { get; } = new List<PatientOpenVisit>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
