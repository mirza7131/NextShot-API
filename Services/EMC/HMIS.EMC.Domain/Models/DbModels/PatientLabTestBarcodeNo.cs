using System;
using System.Collections.Generic;

namespace HMIS.EMC.Domain.Models.DbModels;

public partial class PatientLabTestBarcodeNo
{
    public Guid PatientLabTestBarcodeNoId { get; set; }

    public int HealthFacilityId { get; set; }

    public int LabTestId { get; set; }

    public bool IsOnline { get; set; }

    public int SerialNo { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
