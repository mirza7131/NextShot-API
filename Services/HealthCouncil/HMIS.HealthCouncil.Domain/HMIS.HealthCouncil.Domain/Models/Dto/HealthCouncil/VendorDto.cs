using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{
    public class VendorDto
    {
        public Guid? VendorId { get; set; }

        public string? FirmName { get; set; }

        public string? ContactNo { get; set; }

        public string? Email { get; set; }

        public string? Ntn { get; set; }

        public string? SalesTaxNo { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? BankAccountTitle { get; set; }

        public string? BankAccountNo { get; set; }

        public string? BankName { get; set; }

        public string? BankBranchName { get; set; }

        public string? BankBranchCode { get; set; }

        public int? HealthFacilityId { get; set; }

        public bool? IsActive { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
