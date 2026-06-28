using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblTicket
{
    public long Id { get; set; }

    public int MdId { get; set; }

    public string TicketId { get; set; } = null!;

    public int Sender { get; set; }

    public string Message { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Created { get; set; }
}
