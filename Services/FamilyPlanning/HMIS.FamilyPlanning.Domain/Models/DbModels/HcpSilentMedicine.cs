using System;
using System.Collections.Generic;

namespace HMIS.FamilyPlanning.Domain.Models.DbModels;

public partial class HcpSilentMedicine
{
    public Guid HcpSilentMedicineId { get; set; }

    public int MimsMedicineId { get; set; }

    public string MedicineName { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public int QuantityDispatch { get; set; }

    public int QuantityPrescribed { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }
}
