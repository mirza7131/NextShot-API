using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class InventoryItem
{
    public int InventoryItemId { get; set; }

    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;

    public decimal Price { get; set; }

    public int StockQty { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
    public virtual ICollection<TableSessionInventoryItem> TableSessionInventoryItems { get; set; } = new List<TableSessionInventoryItem>();
}
