using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil
{
    public class MeetingExpendetureDto
    {
        public Guid? MeetingExpendetureId { get; set; }

        public Guid? MeetingDetailId { get; set; }

        public Guid? MeetingDisscussedCategoryId { get; set; }

        public string? ItemName { get; set; }
        public string? Name { get; set; }

        public int? Quantity { get; set; }

        public decimal? PricePerUnit { get; set; }

        public decimal? EstimatedCost { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}
