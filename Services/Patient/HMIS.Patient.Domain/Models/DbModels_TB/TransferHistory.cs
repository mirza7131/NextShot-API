using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class TransferHistory
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? SourceProgramId { get; set; }

    public int? DestinationProgramId { get; set; }

    public int? SourceFacilityId { get; set; }

    public int? DestinationFacilityId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public virtual Program? DestinationProgram { get; set; }

    public virtual Patient? Patient { get; set; }
}
