using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class SocialWelfareForm
{
    public Guid Id { get; set; }

    public Guid FormTypeProfileId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public int? ReferDistrictId { get; set; }

    public Guid? PatientVistId { get; set; }

    public Guid? PatientId { get; set; }

    public string? Mdrcprofession { get; set; }

    public string? MdrcmonthlyIncome { get; set; }

    public string? MdrcfhrdrugAddiction { get; set; }

    public string? MdrcifYesRelation { get; set; }

    public string? MdrcfamilyAttitude { get; set; }

    public string? MdrcpatientAttitude { get; set; }

    public string? Mdrccfodabuse { get; set; }

    public string? MdrcdetailOfCounsellingSessionsSesssionI { get; set; }

    public string? MdrcdetailOfCounsellingSessionsSesssionIi { get; set; }

    public string? MdrcdetailOfCounsellingSessionsSesssionIii { get; set; }

    public string? MdrcmsoprovisionReadingMaterial { get; set; }

    public string? MdrcindoorActivities { get; set; }

    public string? MdrcrecreationalActivities { get; set; }

    public string? MdrcanyOtherMso { get; set; }

    public int? ReferProvinceId { get; set; }

    public int? SessionNo { get; set; }

    public bool? IsVisitClosed { get; set; }

    public int? ReferDivisionId { get; set; }

    public string? Education { get; set; }

    public int? PtPositionInFamily { get; set; }

    public int? NoOfSisters { get; set; }

    public int? NoOfBrothers { get; set; }
}
