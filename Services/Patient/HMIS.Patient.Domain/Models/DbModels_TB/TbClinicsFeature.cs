using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class TbClinicsFeature
{
    public int Id { get; set; }

    public string? HfmisCode { get; set; }

    public bool? GeneExpert { get; set; }

    public bool? SimpleTb { get; set; }
}
