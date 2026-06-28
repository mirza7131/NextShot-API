using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class MedicalTest
{
    public int Id { get; set; }

    public string? TestName { get; set; }

    public string? TestDescription { get; set; }

    public int? ProgramId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid? Guid { get; set; }
}
