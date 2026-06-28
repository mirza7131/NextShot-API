using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.BankDetails
{

    public class BankDetailsDto
    {
        public Guid? HealthFacilityBankDetailId { get; set; }

        public string? Bank { get; set; }

        public string? BranchName { get; set; }

        public string? BranchCode { get; set; }

        public string? AccountTitle { get; set; }

        public string? AccountNo { get; set; }

        public string? BankContact { get; set; }

        public decimal? CurrentBalance { get; set; }

        public decimal? OpeningBalance { get; set; }

        public int? HealthFacilityId { get; set; }
    }
}
