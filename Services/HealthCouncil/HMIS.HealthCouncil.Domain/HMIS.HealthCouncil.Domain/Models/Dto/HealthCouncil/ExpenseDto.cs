using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{
    public class ExpenseDto
    {
        public Guid? ExpenseId { get; set; }

        public Guid? MeetingDetailId { get; set; }

        public Guid? MeetingDisscussedCategoryId { get; set; }

        public Guid? VendorId { get; set; }

        public string? ChequeNo { get; set; }

        public DateTime? ChequeDate { get; set; }

        public decimal? ExpenseAmount { get; set; }

        public int? HealthFacilityId { get; set; }

        public bool? IsActive { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
