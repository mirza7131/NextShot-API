using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.DbModels;

public partial class PatientImage
{
    public Guid PatientImageId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? ImageTypeProfileId { get; set; }

    public Guid? UnknownPatientId { get; set; }

    public Guid? ImageBaseSixtyFourId { get; set; }

    public Guid? ProfileTypeId { get; set; }

    public string? ImageUrl { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte? ActionTypeId { get; set; }
}
