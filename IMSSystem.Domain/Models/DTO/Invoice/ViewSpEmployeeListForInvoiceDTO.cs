using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSSystem.Domain.Models.DTO.Invoice
{
    public class ViewSpEmployeeListForInvoiceDTO
    {
        public Guid HfSpEmployeeId { get; set; }

        public Guid? SpId { get; set; }

        public string? ServiceProvider { get; set; }

        public int? HfId { get; set; }

        public string? HealthFacility { get; set; }

        public Guid? ServiceTypeId { get; set; }

        public string? Service { get; set; }

        public Guid? DesignationId { get; set; }

        public string? Designation { get; set; }

        public int? ShiftId { get; set; }

        public string? Shifts { get; set; }

        public int? EmploymentTypeId { get; set; }

        public string? EmploymentTypes { get; set; }

        public Guid? EmployeeId { get; set; }

        public string? Name { get; set; }
        public Guid? ReplacementOf { get; set; }
        public bool? IsUpdated { get; set; }
    }
    public class FilterEmployeeDTO
    {
       // public Guid? SpId { get; set; }

        public int? HfId { get; set; }

        public Guid? ServiceTypeId { get; set;}
    }
}
