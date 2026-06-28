using HMIS.HealthCouncil.Domain.Models.DTO.HealthFacility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.DTO.Budget
{
    public class ViewAllocateBudgetDto
    {
        public Guid BudgetId { get; set; }
        public bool? IsBudgetAllocated { get; set; }
        public bool? IsChequeIssue { get; set; }
        public DateTime? ChequeIssueDate { get; set; }
        public decimal? ReleasedAmount { get; set; }
        public decimal? AllocatedAmount { get; set; }
        public decimal? CurrentBalance { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? RequestForBudget { get; set; }
        public decimal? IssuedBudget { get; set; }
        public decimal? Expenses { get; set; }

        public int? HealthFacilityId { get; set; }
        public string? HealthFacilityName { get; set; }

        public bool IsActive { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public byte ActionTypeId { get; set; }

        public string? BankContact { get; set; }
        public string? Bank { get; set; }
        public string? BranchName { get; set; }
        public string? BranchCode { get; set; }
        public string? AccountTitle { get; set; }
        public string? AccountNo { get; set; }
        public Guid? ChequeReceivedStatusProfileId { get; set; }
        public DateTime? ChequeReceivedDate { get; set; }
        public string? ChequeNo { get; set; }
        public string? CourierCompany { get; set; }
        public string? DiaryNo { get; set; }
        public DateTime? CourierDispatchDate { get; set; }

    }

    public class ResponseAllocateBudget
    {
        public List<ViewAllocateBudgetDto> ListAllocateBudget { get; set; }
        public List<ViewHealthFacilityDto> ListHealthFaciity { get; set; }

    }
}
