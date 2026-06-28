using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{
    public class ExpenseListDto
    {
        public Guid? VendorId { get; set; }
        public string? FirmName { get; set; }
        public string? ChequeNo { get; set; }
        public string? MeetingNo { get; set; }
        public DateTime? MeetingDate { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string? CategoryName { get; set; }
        public int? ExpenseAmount { get; set; }
    }
}
