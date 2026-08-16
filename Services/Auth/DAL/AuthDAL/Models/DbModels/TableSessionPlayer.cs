using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.DbModels;

public partial class TableSessionPlayer
{
    public int TableSessionPlayerId { get; set; }

    public int TableSessionId { get; set; }

    public int? ClubCustomerId { get; set; }

    public string PlayerName { get; set; } = null!;

    public string? PhoneNo { get; set; }

    public bool IsWalkIn { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual TableSession TableSession { get; set; } = null!;

    public virtual ClubCustomer? ClubCustomer { get; set; }
}
