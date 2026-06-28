using System;
using System.Collections.Generic;

namespace HMIS.Data.SyncUtility.Models.DbModels;

public partial class ViewGetPatientBaseSixtyFour
{
    public Guid? PatientId { get; set; }

    public Guid PatientImageId { get; set; }

    public Guid ImageBaseSixtyFourId { get; set; }

    public Guid? UnknownPatientId { get; set; }

    public Guid? ProfileTypeId { get; set; }

    public string? Base64 { get; set; }
}
