using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSSystem.Domain.Models.DTO.Invoice
{
    public class ValidationDTO
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
            public bool? IsApproved { get; set; }
            public bool? IsRejected { get; set; }
        
    }

}
