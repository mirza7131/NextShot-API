using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class TbPatientsList
{
    public Guid PatientId { get; set; }

    public string? Mrno { get; set; }

    public string? FullName { get; set; }

    public string Cnic { get; set; } = null!;

    public int? Age { get; set; }

    public string? MobileNo { get; set; }

    public string? FormType { get; set; }

    public int? PatientDepartmentLookupId { get; set; }

    public int? PatientSectionLookupId { get; set; }
}
