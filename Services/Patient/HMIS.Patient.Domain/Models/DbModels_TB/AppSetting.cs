using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class AppSetting
{
    public int? Id { get; set; }

    public string? AppVersion { get; set; }
}
