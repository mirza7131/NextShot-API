using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.DbModels
{
    public partial class InventorySaleItem
    {
        public int InventorySaleItemId { get; set; }
        public int InventorySaleId { get; set; }
        public int InventoryItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual InventorySale InventorySale { get; set; } = null!;
    }
}
