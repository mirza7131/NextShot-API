using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSSystem.Domain.Models.DTO.Invoice
{
    public class InvoiceWithEmployeeDetailsDTO
    {
        public string? InvoiceNumber { get; set; }
        public Guid? SpId { get; set; }
        public string? Logo { get; set; }
        public string? Banner { get; set; }
        public string? SpName { get; set; }
        public Guid? ServiceTypeId { get; set; }
        public string? Service { get; set; }
        public int? HfId { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? HealthFacilityTypeCode { get; set; }
        public string? HFType { get; set; }
        public string? Month { get; set; } 
        public DateTime? DueDate { get; set; }
        public string? Comments { get; set; }
        public DateTime? IssuedOn { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmploymentType { get; set; }
    }
}
