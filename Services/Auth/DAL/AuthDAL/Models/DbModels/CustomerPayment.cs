using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.DbModels;

public partial class CustomerPayment
{
    public int CustomerPaymentId { get; set; }

    public int ClubCustomerId { get; set; }

    public int? TableSessionId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentType { get; set; } = null!;

    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual ClubCustomer ClubCustomer { get; set; } = null!;

    public virtual TableSession? TableSession { get; set; }

    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal DueAmount { get; set; }
    public string? PaymentStatus { get; set; }
    public decimal CashAmount { get; set; }
    public decimal CardAmount { get; set; }

    public int? InventorySaleId { get; set; }
    public string? ReceiptNo { get; set; }
  

}
