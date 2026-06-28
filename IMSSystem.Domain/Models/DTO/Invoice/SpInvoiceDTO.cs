using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSSystem.Domain.Models.DTO.Invoice
{
    public class SpInvoiceDTO
    {
        public int InvoiceId { get; set; }

        public string? InvoiceNumber { get; set; }

        public Guid? SpId { get; set; }

        public int? HfId { get; set; }

        public Guid? ServiceTypeId { get; set; }

        public string? Month { get; set; }

        public DateTime? DueDate { get; set; }

        public bool? IsActive { get; set; }

        public Guid? IssuedBy { get; set; }

        public DateTime? IssuedOn { get; set; }

        public bool? IsReIssued { get; set; }

        public Guid? ReIssuedBy { get; set; }

        public DateTime? ReIssuedOn { get; set; }
    }
    public class SaveEmployeeDTO
    {
        public Guid HfSpEmployeeId { get; set; }

        public Guid? SpId { get; set; }

        public Guid? ServiceTypeId { get; set; }

        public int? HfId { get; set; }

        public Guid? DesignationId { get; set; }

        public int? EmploymentTypeId { get; set; }

        public int? ShiftId { get; set; }

        public Guid? EmployeeId { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CraetedBy { get; set; }
        public bool? IsUpdated { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }
        public Guid? ReplacementOf { get; set; }
        public void AssignGuid()
        {
            HfSpEmployeeId = Guid.NewGuid(); 
        }
    }
    public class GetEmployeeToEditRequest
    {
        public Guid HfSpEmployeeId { get; set; }
    }
    public class InsertComments
    {
        public string? InvoiceNumber { get; set; }
        public string? Comments { get; set; }
    }
}
