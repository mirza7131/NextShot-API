using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.DbModels;

public partial class TableSessionInventoryItem
{
    public int TableSessionInventoryItemId { get; set; }

    public int TableSessionId { get; set; }

    public int InventoryItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string? BuyerName { get; set; }

    public int? ClubCustomerId { get; set; }

    public decimal TotalAmount { get; private set; }

    public DateTime CreatedOn { get; set; }

    public virtual TableSession TableSession { get; set; } = null!;

    public virtual InventoryItem InventoryItem { get; set; } = null!;
}
