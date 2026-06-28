using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblSample
{
    public long Id { get; set; }

    public string? Number { get; set; }

    public int? UserId { get; set; }

    public int? UserHospital { get; set; }

    public int? Created { get; set; }

    public int? Pid { get; set; }

    public int? Updated { get; set; }

    public int? UpdatedBy { get; set; }

    public int? ActionId { get; set; }

    public int? LabReceptionistId { get; set; }

    public string? IsDispatch { get; set; }

    public string? IsReception { get; set; }

    public string? IsLabBatch { get; set; }

    public string? IsSampleSvr { get; set; }

    public string? SampleTakenWith { get; set; }
}
