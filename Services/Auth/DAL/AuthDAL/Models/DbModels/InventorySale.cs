using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.DbModels
{
    public partial class InventorySale
    {
        public int InventorySaleId { get; set; }
        public string ReceiptNo { get; set; } = null!;

        public int? ClubCustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? PhoneNo { get; set; }

        public int InventoryItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal? NetAmount { get; set; }

        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }

        public string PaymentStatus { get; set; } = null!;

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<InventorySaleItem> InventorySaleItems { get; set; } = new List<InventorySaleItem>();
    }
}
