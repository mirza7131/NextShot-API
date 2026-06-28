using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.PaginationDto
{
    public class StockCheckFilter
    {
        public int? MedicineId { get; set; }
        public Guid? MimsBranchId { get; set; }
    }

    public class StockCheckboxCheckFilter
    {
        public Guid? MimsBranchId { get; set; }
        public bool? IsBatchWise { get; set; }
        public int? MedicineId { get; set; }
        public DateTime? NearestExpire {  get; set; }
        public DateTime? IsExpired { get; set; }
        public DateTime? FromDate { get; set;}
        public DateTime? ToDate { get;set;}



        public Guid? GuidId { get; set; }
        public bool? IsDueAmount { get; set; }
        public bool? IsTotalAmount { get; set; }
        public bool? IsAllAmount { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

    }
}
