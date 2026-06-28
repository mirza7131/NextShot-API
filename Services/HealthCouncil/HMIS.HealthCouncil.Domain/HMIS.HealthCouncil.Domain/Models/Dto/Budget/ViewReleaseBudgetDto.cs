using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.Budget
{
    public class ViewReleaseBudgetDto
    {
        public Guid? BudgetId { get; set; }
        public int? HealthFacilityId { get; set; }

        public string? ChequeImage { get; set; }
        public string? HealthFacilityName { get; set; }

        public bool? BankAccountStatus { get; set; }

        public decimal? AllocatedAmount { get; set; }

        public decimal? ReleaseAmount { get; set; }

        public int? ChequeStatus { get; set; }
        public bool? IsChequeIssue { get; set; }
    }
}
