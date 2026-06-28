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
}
