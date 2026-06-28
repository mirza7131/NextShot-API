using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.Budget
{
    public class BudgetDto
    {
        public Guid? BudgetId { get; set; }

        public decimal? AllocatedAmount { get; set; }

        public DateTime? ChequeIssueDate { get; set; }

        public string? CourierCompany { get; set; }

        public DateTime? CourierDispatchDate { get; set; }

        public string? DiaryNo { get; set; }

        public int? ChequeNo { get; set; }

        public Guid? ChequeReceivedStatusProfileId { get; set; }

        public DateTime? ChequeReceivedDate { get; set; }

        public int? HealthFacilityId { get; set; }
    }


    public class ChequeStatusDto
    {
        public Guid BudgetId { get; set; }
        public Guid ChequeStatusProfileId { get; set; }
        public int AllocatedAmount { get; set; }
        public string ProfileName { get; set; }

    }
}
