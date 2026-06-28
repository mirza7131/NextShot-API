using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class Mlc
{
    public Guid Mlcid { get; set; }

    public Guid? MlctypeProfileId { get; set; }

    public Guid PatientId { get; set; }

    public Guid PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientStatusProfileId { get; set; }

    public int? HealthFacilityId { get; set; }

    public Guid? DoctorId { get; set; }

    public string? CaseAgainst { get; set; }

    public Guid? McdtypeProfileId { get; set; }

    public Guid? PcdtypeProfileId { get; set; }

    public string? Mlcno { get; set; }

    public string? BookNo { get; set; }

    public string? PoliceDistrict { get; set; }

    public string? ConsentFile { get; set; }

    public string? ReasonForChangeDoctor { get; set; }

    public string? PoliceDocketOne { get; set; }

    public string? PoliceDocketTwo { get; set; }

    public string? PoliceDocketThree { get; set; }

    public Guid? ImageTypeProfileId { get; set; }

    public long? ReportCounts { get; set; }

    public string? QrCodeImagePath { get; set; }

    public bool? IsFinalReport { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public bool? IsBookNoGenerated { get; set; }

    public virtual ICollection<MlcbodyIdentifierInfo> MlcbodyIdentifierInfos { get; } = new List<MlcbodyIdentifierInfo>();

    public virtual ICollection<MlcpoliceInfo> MlcpoliceInfos { get; } = new List<MlcpoliceInfo>();

    public virtual ICollection<Mlcpostmortem> Mlcpostmortems { get; } = new List<Mlcpostmortem>();

    public virtual ICollection<MlcsvinitialInfo> MlcsvinitialInfos { get; } = new List<MlcsvinitialInfo>();

    public virtual ICollection<MlebasicInfo> MlebasicInfos { get; } = new List<MlebasicInfo>();

    public virtual Patient Patient { get; set; } = null!;
}
