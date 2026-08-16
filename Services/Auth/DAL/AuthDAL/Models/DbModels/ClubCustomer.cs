using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.DbModels;

[Table("ClubCustomers", Schema = "dbo")]
public partial class ClubCustomer
{
    public int ClubCustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? PhoneNo { get; set; }

    [Column("BalanceAmount")]
    public decimal BalanceAmount { get; set; }
    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual ICollection<TableSessionPlayer> TableSessionPlayers { get; set; } = new List<TableSessionPlayer>();

    public virtual ICollection<CustomerPayment> CustomerPayments { get; set; } = new List<CustomerPayment>();
}
