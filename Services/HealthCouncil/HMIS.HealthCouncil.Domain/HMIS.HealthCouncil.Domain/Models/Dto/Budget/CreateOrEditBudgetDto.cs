using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.DTO.Budget
{
    public class CreateOrEditBudgetDto
    {
        public Guid? BudgetId { get; set; }

        public bool? IsChequeIssue { get; set; }
        public bool? IsBudgetAllocated { get; set; }
        public decimal? AllocatedAmount { get; set; }

        public int? HealthFacilityId { get; set; }
        public List<int>? HealthFacilityIds { get; set; }
        public bool IsActive { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid? DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public byte ActionTypeId { get; set; }

    }
}
