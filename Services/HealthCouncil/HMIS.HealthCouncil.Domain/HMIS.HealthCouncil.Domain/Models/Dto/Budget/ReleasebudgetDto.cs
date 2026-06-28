using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.Budget
{
    public class ReleasebudgetDto
    {
        public int? HealthFacilityId { get; set; }
        public Guid? BudgetId { get; set; }
        public int? AllocatedAmount { get; set; }
        public int? ReleaseAmount { get; set; }
        public string? CourierCompany { get; set; }
        public DateTime? CourierDispatchDate { get; set; }
        public string? DiaryNo { get; set; }
        public int? ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
    }



    public class ChequeImageDto
    {
        public Guid? BudgetId { get; set; }
        public string? ChequeImageBase64 { get; set; }
    }
}
