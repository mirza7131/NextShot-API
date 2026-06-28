using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class Dfprofile
{
    public int DfprofileId { get; set; }

    public int DtcprofileId { get; set; }

    public string HfmisCode { get; set; } = null!;

    public string Dfname { get; set; } = null!;

    public string Dfemail { get; set; } = null!;

    public string DfmobileNo { get; set; } = null!;

    public string? Dfcnic { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid Guid { get; set; }

    public virtual Dtcprofile Dtcprofile { get; set; } = null!;
}
