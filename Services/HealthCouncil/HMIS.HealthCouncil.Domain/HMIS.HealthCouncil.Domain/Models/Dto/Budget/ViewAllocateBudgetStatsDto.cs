using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.DTO.Budget
{
    public class ViewAllocateBudgetStatsDto
    {
        public int? HealthFacilityTypeId { get; set; }
        public string? HealthFacilityType { get; set; }
        public int? HealthFacilityCount { get; set; }
        public int? BankAccountCount { get; set; }
        public decimal? InitialBalance { get; set; }
        public decimal? CurrentBankBalance { get; set; }
        public decimal? IssuedBudget { get; set; }
        public decimal? RequestForBudget { get; set; }
        public decimal? Expenses { get; set; }

    }
}
