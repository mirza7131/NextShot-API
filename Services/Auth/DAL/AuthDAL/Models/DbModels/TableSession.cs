using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class TableSession
{
    public int TableSessionId { get; set; }

    public int TableNo { get; set; }

    public string TableName { get; set; } = null!;

    public string TableType { get; set; } = null!;

    public string? CustomerName { get; set; }

    public int PlayerCount { get; set; }

    public string SessionMode { get; set; } = null!;

    public decimal HourlyRate { get; set; }

    public decimal? GameRate { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public decimal TableAmount { get; set; }

    public decimal InventoryAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TotalAmount { get; private set; }

    public decimal PaidAmount { get; set; }

    public decimal DueAmount { get; set; }

    public string Status { get; set; } = null!;

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
    public int? ClubCustomerId { get; set; }

    public string? CustomerPhone { get; set; }

    public string? PlayerOneName { get; set; }

    public string? PlayerTwoName { get; set; }

    public decimal MinuteRate { get; set; }

    public int GameCount { get; set; }

    public decimal GameAmount { get; set; }

    public decimal GrossAmount { get; set; }

    public decimal NetAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string? ReceiptNo { get; set; }

    public virtual ICollection<TableSessionPlayer> TableSessionPlayers { get; set; } = new List<TableSessionPlayer>();

    public virtual ICollection<TableSessionInventoryItem> TableSessionInventoryItems { get; set; } = new List<TableSessionInventoryItem>();

    public virtual ICollection<TableSessionGame> TableSessionGames { get; set; } = new List<TableSessionGame>();

    public virtual ICollection<CustomerPayment> CustomerPayments { get; set; } = new List<CustomerPayment>();
}
