using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.DbModels;

public partial class TableSessionGame
{
    public int TableSessionGameId { get; set; }

    public int TableSessionId { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual TableSession TableSession { get; set; } = null!;
}
