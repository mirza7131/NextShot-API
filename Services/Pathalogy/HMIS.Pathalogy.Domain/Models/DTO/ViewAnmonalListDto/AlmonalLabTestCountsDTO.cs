using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.ViewAnmonalListDto
{
    public class AlmonalLabTestCountsDTO
    {
        public int? TotalRecommended { get; set; }
        public int? PaidTests { get; set; }
        public int? FreeTests { get; set; }
        public int? FeeCollected { get; set; }
        public int? FeeToBeCollected { get; set; }
        public int? AmountToBePaid { get; set; }
        public int? AmountReceived { get; set; }
        public int? TotalRefund { get; set; }
        public int? Balance { get; set; }
        public int? UserAmountReceived { get; set; }
        public int? UserTotalRefund { get; set; }
        public int? UserBalance { get; set; }
    }
}
