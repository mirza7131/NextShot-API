using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblMedDeliveryLog
{
    public int Id { get; set; }

    public int? HospitalId { get; set; }

    public int? UserId { get; set; }

    public int? Created { get; set; }

    public int PatientId { get; set; }

    public string? PrescriptionNo { get; set; }

    public long? ConsignmentNo { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? Consignee { get; set; }

    public int? DeliveredBy { get; set; }

    public string? ReceivedBy { get; set; }

    public string? Location { get; set; }

    public string? Status { get; set; }

    public string? DeliveredTcsDate { get; set; }

    public int? NoOfDosage { get; set; }

    public string PendingDeliveryAlert { get; set; } = null!;

    public string CloseCase { get; set; } = null!;

    public string IsReceivedShipment { get; set; } = null!;

    public int? ReceivedHospitalId { get; set; }

    public string IsDemote { get; set; } = null!;

    public string? MoStatus { get; set; }

    public int? MoDeliver { get; set; }
}
