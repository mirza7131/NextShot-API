using System;
using System.Collections.Generic;

namespace HMIS.Dashboard.Domain.Models.TransDbModels;

public partial class SocialWelfareAssignDoctor
{
    public Guid SwDoctorAssignId { get; set; }

    public Guid DoctorId { get; set; }

    public Guid PatientId { get; set; }

    public Guid VisitId { get; set; }

    public Guid SocialWellfareFormId { get; set; }

    public int ProvinceId { get; set; }

    public int DivisionId { get; set; }

    public int DistrictId { get; set; }

    public bool? IsAssignDoctor { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public DateTime? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public int? ActionTypeId { get; set; }
}
