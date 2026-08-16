using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.DbModels
{
    public partial class ClubCustomerPayment
    {
        public int ClubCustomerPaymentId { get; set; }
        public int ClubCustomerId { get; set; }
        public int? TableSessionId { get; set; }
        public string? PlayerName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string PaymentType { get; set; } = "Game";
        public string PaymentStatus { get; set; } = "Pending";
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
