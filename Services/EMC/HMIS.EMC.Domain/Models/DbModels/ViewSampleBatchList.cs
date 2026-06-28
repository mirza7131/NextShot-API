using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class ViewSampleBatchList
{
    public string? BatchNumber { get; set; }

    public DateTime? BatchCreatedOn { get; set; }

    public string? BatchCreatedBy { get; set; }

    public DateTime? BatchResultUploadedOn { get; set; }

    public int? SamplesCount { get; set; }
}
