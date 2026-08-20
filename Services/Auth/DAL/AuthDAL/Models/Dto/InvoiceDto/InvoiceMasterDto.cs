using AuthDAL.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.InvoiceDto
{
    public class CreateAndEditInvoiceDto
    {
        public CreateAndEditInvoiceDto()
        {
            invoiceMasterDto = new InvoiceMasterDto();
            invoiceDocumentListDto = new List<InvoiceDocumentListDto>();
            invoiceItemListDto = new List<InvoiceItemListDto>();
        }


        public InvoiceMasterDto invoiceMasterDto { get; set; }
        public List<InvoiceDocumentListDto> invoiceDocumentListDto { get; set; }
        public List<InvoiceItemListDto> invoiceItemListDto { get; set; }
    }
    public class InvoiceMasterDto
    {
        public Guid? InvoiceMasterId { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? BillToName { get; set; }
        public string? PrintBy { get; set; }
        public int? ApplyDiscount { get; set; }
        public int? ReceivedAmount { get; set; }
        public int? HealthfacilityId { get; set; }
        public int? TotalAmount { get; set; }
        public int? DueAmount { get; set; }
        public int? NetAmount { get; set; }
        public string? TermAndCondition { get; set; }
        //public List<InvoiceDocumentListDto> InvoiceDocumentListDto { get; set; }
        //public List<InvoiceItemListDto> InvoiceItemListDto { get; set; }

        //public bool? IsActive { get; set; }

        //public bool? IsDelete { get; set; }

        //public DateTime? CreatedOn { get; set; }

        //public Guid? CreatedBy { get; set; }

        //public DateTime? UpdatedOn { get; set; }

        //public Guid? UpdatedBy { get; set; }

        //public DateTime? DeletedOn { get; set; }

        //public Guid? DeletedBy { get; set; }

        //public DateTime? DueAmountPayOn { get; set; }

        //public Guid? DueAmountPayBy { get; set; }

        //public virtual ICollection<InvoiceDocumentList> InvoiceDocumentLists { get; } = new List<InvoiceDocumentList>();

        //public virtual ICollection<InvoiceItemList> InvoiceItemLists { get; } = new List<InvoiceItemList>();





    }

    public class InvoiceDocumentListDto
    {
        public Guid? InvoiceDocumentId { get; set; }
        public Guid? InvoiceMasterId { get; set; }
        public string? DocumentName { get; set; }

        //public bool? IsActive { get; set; }
        //public bool? IsDelete { get; set; }
        //public DateTime? CreatedOn { get; set; }
        //public Guid? CreatedBy { get; set; }
        //public DateTime? UpdatedOn { get; set; }
        //public Guid? UpdatedBy { get; set; }
        //public DateTime? DeletedOn { get; set; }
        //public Guid? DeletedBy { get; set; }
        //public virtual InvoiceMaster? InvoiceMaster { get; set; }
    }

    public class InvoiceItemListDto
    {
        public Guid? ItemListId { get; set; }
        public Guid? InvoiceMasterId { get; set; }
        public string? ItemName { get; set; }
        public int? Quantity { get; set; }
        public int? Rate { get; set; }
        public int? ItemTotalAmount { get; set; }

        //public bool? IsActive { get; set; }
        //public bool? IsDelete { get; set; }
        //public DateTime? CreatedOn { get; set; }
        //public Guid? CreatedBy { get; set; }
        //public DateTime? UpdatedOn { get; set; }
        //public Guid? UpdatedBy { get; set; }
        //public DateTime? DeletedOn { get; set; }
        //public Guid? DeletedBy { get; set; }
        //public DateTime? DueAmountPayOn { get; set; }
        //public Guid? DueAmountPayBy { get; set; }
        //public virtual InvoiceMaster? InvoiceMaster { get; set; }
    }


    public class getInvoiceAutoCompleteDto
    {
        public List<InvoiceMasterDropdownDto>? invoiceMasterDropdownDto { get; set; }
        public List<InvoiceItemDropdownDto>? invoiceItemDropdownDto { get; set; }
        public List<InvoiceReqDocumentDropdownDto>? invoiceReqDocumentDropdownDto { get; set; }
        public List<InvoiceTermAndConDropdownDto>? invoiceTermAndConDropdownDto { get; set; }
    }
    public class InvoiceMasterDropdownDto
    {
        public Guid? InvoiceMasterId { get; set; }
        public string? BillToName { get; set; }
    }
    public class InvoiceItemDropdownDto
    {
        public Guid? ItemListId { get; set; }
        public string? ItemName { get; set; }
    }

    public class InvoiceReqDocumentDropdownDto
    {
        public Guid? InvoiceMasterId { get; set; }
        public string? DocumentName { get; set; }
    }

    public class InvoiceTermAndConDropdownDto
    {
        public Guid? InvoiceMasterId { get; set; }
        public string? TermAndCondition { get; set; }
    }


    public class InvoiceDashboardCountDto
    {
        public int? TotalCount { get; set; }
        public int? TotalNetAmount { get; set; }
        public int? TotalDueAmount { get; set; }
        public int? TotalReceivedAmount { get; set; }

    }

    public class InvoiceDashboardCountAndListDto
    {
        public List<InvoiceMaster>? invoiceMaster { get; set; }
        public List<InvoiceDashboardCountDto>? invoiceDashboardCountDto { get; set; }
    }

    public class updateDueAmountDto
    {
        public Guid? InvoiceMasterId { get; set; }
        public int DueAmountPay { get; set; }
    }



    public class CreateInventoryItemDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public string? CreatedBy { get; set; }
    }

    public class UpdateInventoryItemDto
    {
        public int InventoryItemId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQty { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class ChangeInventoryStatusDto
    {
        public int InventoryItemId { get; set; }
        public bool IsActive { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class ReduceInventoryStockDto
    {
        public int InventoryItemId { get; set; }
        public int Quantity { get; set; }
        public string? UpdatedBy { get; set; }
    }



    public class CreateClubCustomerDto
    {
        public string CustomerName { get; set; } = null!;
        public string? PhoneNo { get; set; }
    }

    public class StartTableSessionDto
    {
        public int TableNo { get; set; }
        public string TableName { get; set; } = null!;
        public string TableType { get; set; } = null!;

        public int? ClubCustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }

        public string SessionMode { get; set; } = "Time";
        public decimal MinuteRate { get; set; } = 0.50m;
        public decimal HourlyRate { get; set; } = 30;
        public decimal GameRate { get; set; } = 15;
    }

    public class AddSessionPlayerDto
    {
        public int TableSessionId { get; set; }
        public int? ClubCustomerId { get; set; }
        public string PlayerName { get; set; } = null!;
        public string? PhoneNo { get; set; }
        public bool IsWalkIn { get; set; }
    }

    public class AddSessionInventoryDto
    {
        public int TableSessionId { get; set; }
        public int InventoryItemId { get; set; }
        public string? BuyerName { get; set; }
        public int? ClubCustomerId { get; set; }
        public int Quantity { get; set; }
    }

    public class AddSessionGameDto
    {
        public int TableSessionId { get; set; }
        public decimal GameRate { get; set; } = 15;
    }

//    public class EndTableSessionDto
//    {
//        public int TableSessionId { get; set; }
//        public decimal DiscountAmount { get; set; }
//        public decimal PaidAmount { get; set; }

//        public List<PlayerPaymentDto>? PlayerPayments { get; set; }
//    }

//public class PlayerPaymentDto
//    {
//        public string PlayerName { get; set; } = null!;
//        public decimal Amount { get; set; }
//        public decimal DiscountAmount { get; set; }
//        public decimal PaidAmount { get; set; }
//        public decimal DueAmount { get; set; }
//    }
    public class ClubCustomerSearchDto
    {
        public int ClubCustomerId { get; set; }
        public string CustomerName { get; set; }
        public string? PhoneNo { get; set; }
    }
    public class CancelTableSessionDto
    {
        public int TableSessionId { get; set; }
        public int Id { get; set; }
    }
    public class UpdateClubCustomerDto
    {
        public int ClubCustomerId { get; set; }
        public string CustomerName { get; set; } = "";
        public string? PhoneNo { get; set; }
    }
    public class DeleteClubCustomerDto
    {
        public int? Id { get; set; }
        public int? ClubCustomerId { get; set; }
    }

    public class CreateClubTableDto
    {
        public int TableNo { get; set; }
        public string TableName { get; set; } = "";
        public string TableType { get; set; } = ""; // Snooker / Billiard
        public decimal HourlyRate { get; set; }
        public decimal GameRate { get; set; }
        public decimal DoubleHourlyRate { get; set; }
        public decimal DoubleGameRate { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateClubTableDto : CreateClubTableDto
    {
        public int ClubTableId { get; set; }
    }

    public class DeleteClubTableDto
    {
        public int? Id { get; set; }
        public int? ClubTableId { get; set; }
    }

    public class PlayerPaymentDto
    {
        public int? ClubCustomerId { get; set; }
        public string PlayerName { get; set; } = null!;
        public int GameCount { get; set; }
        public decimal InventoryAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
    }
    public class EndTableSessionDto
    {
        public int TableSessionId { get; set; }
        public string? PlayType { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal MinuteRate { get; set; }
        public decimal GameRate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }

        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public List<EndTableSessionPlayerPaymentDto>? PlayerPayments { get; set; }
     
    }
    public class EndTableSessionPlayerPaymentDto
    {
        public int? ClubCustomerId { get; set; }
        public string? PlayerName { get; set; }
        public int GameCount { get; set; }
        public decimal TimeAmount { get; set; }
        public decimal InventoryAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
    }
    public class PayCustomerPendingAmountDto
    {
        public int ClubCustomerId { get; set; }
        public decimal PaidAmount { get; set; }
    }


    public class PendingInventoryItemDto
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? BuyerName { get; set; }
    }

    public class CustomerPendingPaymentHistoryDto
    {
        public int CustomerPaymentId { get; set; }
        public int ClubCustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? PhoneNo { get; set; }
        public int? TableSessionId { get; set; }
        public int? InventorySaleId { get; set; }
        public string? ReceiptNo { get; set; }
        public string? Players { get; set; }
        public string? TotalTime { get; set; }
        public decimal TableTimeAmount { get; set; }
        public decimal InventoryAmount { get; set; }
        public List<PendingInventoryItemDto> InventoryItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentType { get; set; }
        public DateTime? CreatedOn { get; set; }
    }


    


    public class CreateInventorySaleItemDto
    {
        public int InventoryItemId { get; set; }
        public string ItemName { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateInventorySaleDto
    {
        public int? ClubCustomerId { get; set; }
        public string CustomerName { get; set; } = "";
        public string? PhoneNo { get; set; }

        public int InventoryItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public List<CreateInventorySaleItemDto> Items { get; set; } = new();

        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
    }

}
