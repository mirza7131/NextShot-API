using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class VVisitDetail
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public int? ProgramId { get; set; }

    public string? CurrentStatus { get; set; }

    public string? CurrentStage { get; set; }

    public string? FacilityCode { get; set; }

    public string? DivisionName { get; set; }

    public string? DistrictName { get; set; }

    public string? TehsilName { get; set; }

    public string? Name { get; set; }

    public string? FullName { get; set; }

    public bool? IsTransferred { get; set; }

    public int? TransferredLogId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public int? VisitCount { get; set; }

    public Guid? Guid { get; set; }

    public string? VisitPurpose { get; set; }
}
