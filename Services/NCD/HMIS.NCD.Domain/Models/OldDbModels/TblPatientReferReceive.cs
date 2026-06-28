using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblPatientReferReceive
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? SelfCnic { get; set; }

    public string? MrnNo { get; set; }

    public string? FatherName { get; set; }

    public string? Dob { get; set; }

    public int? Age { get; set; }

    public int? Gender { get; set; }

    public string? ContactNoSelf { get; set; }

    public int? DivisionId { get; set; }

    public int? DistrictId { get; set; }

    public int? TehsilId { get; set; }

    public int? HospitalId { get; set; }

    public string? ReqHospital { get; set; }

    public string? ReqDept { get; set; }

    public string? ReceiveDate { get; set; }

    public string ReferStatus { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public int? Created { get; set; }

    public string? ReferReason { get; set; }
}
