using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.DbModels;

public partial class SampleTransportByLhw
{
    public Guid SampleTransportByLhwId { get; set; }

    public Guid? PatientId { get; set; }

    public Guid? PatientVisitId { get; set; }

    public Guid? PatientDiagnoseId { get; set; }

    public Guid? PatientLabTestId { get; set; }

    public string? NameOfLhw { get; set; }

    public string? ContactOfLhw { get; set; }

    public string? CnicofLhw { get; set; }

    public string? CatchmentAreaOfLhw { get; set; }

    public string? NameOfLhs { get; set; }

    public string? ContactOfLhs { get; set; }

    public string? CnicofLhs { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? Updatedby { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }
}
