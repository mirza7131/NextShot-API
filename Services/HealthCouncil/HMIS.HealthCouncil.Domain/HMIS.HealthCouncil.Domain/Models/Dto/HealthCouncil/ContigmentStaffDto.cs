using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{
    public class ContigmentStaffDto
    {
        public Guid ContignetStaffId { get; set; }

        public string? Name { get; set; }

        public string? FatherName { get; set; }

        public string? PhoneNo { get; set; }

        public string? Cnic { get; set; }

        public decimal? Salary { get; set; }

        public int? HealthFacilityId { get; set; }

        public DateTime? ContractStartDate { get; set; }

        public DateTime? ContractExpiryDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }

        public string? BankBranchName { get; set; }

        public string? BankBranchCode { get; set; }

        public string? BankAccountTitle { get; set; }

        public string? BankAccountNo { get; set; }

        public string? BankName { get; set; }
    }
}
