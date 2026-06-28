using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class ImportExcel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }
}
