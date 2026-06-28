using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class DiagnosisFacility
{
    public int Id { get; set; }

    public string? HfmisCode { get; set; }

    public bool? Spumicroscope { get; set; }

    public bool? GeneXpert { get; set; }

    public bool? Lpa { get; set; }

    public bool? Culture { get; set; }

    public bool? Dst { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletionDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }

    public string? DivisionCode { get; set; }

    public string? DistrictCode { get; set; }

    public string? TehsilCode { get; set; }

    public int OldTbHfId { get; set; }
}
