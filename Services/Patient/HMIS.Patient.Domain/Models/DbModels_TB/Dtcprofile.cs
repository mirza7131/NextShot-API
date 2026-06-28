using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Dtcprofile
{
    public int DtcprofileId { get; set; }

    public string DistrictCode { get; set; } = null!;

    public string Dtcname { get; set; } = null!;

    public string Dtcemail { get; set; } = null!;

    public string DtcmobileNo { get; set; } = null!;

    public string? Dtccnic { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Dfprofile> Dfprofiles { get; } = new List<Dfprofile>();
}
