using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblCbcParameter
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public int? Hemoglobin { get; set; }

    public int? Ast { get; set; }

    public int? Alt { get; set; }

    public int? Platelet { get; set; }

    public int? Tlc { get; set; }

    public string? Apri { get; set; }

    public int? ViralCount { get; set; }

    public string? PcrResult { get; set; }

    public int? SampleId { get; set; }

    public string? BaselineType { get; set; }

    public int? FollowUpNo { get; set; }

    public string? LabName { get; set; }

    public string? OtherLabName { get; set; }

    public string? ResultType { get; set; }

    public int? Created { get; set; }

    public int? Updated { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
