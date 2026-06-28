using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.IndentDetailDto
{
    public class CreateOrEditIndentDetailDto
    {
        public Guid? IndentDetailId { get; set; }

        public Guid? IndentMasterId { get; set; }

        public string? BatchNo { get; set; }

        public long? MedicineId { get; set; }

        public string? MedicineName { get; set; }

        public int? MedicineTypeId { get; set; }

        public int? FundingSourceId { get; set; }

        public DateTime? MedicineMfgDate { get; set; }

        public DateTime? MedicineExpDate { get; set; }

        public string? Remarks { get; set; }

        public decimal? RequestedQty { get; set; }

        public decimal? IssuedQty { get; set; }

        public decimal? ReceivedQty { get; set; }

        public decimal? UnitPrice { get; set; }

        public bool? IsSmlmedicine { get; set; }

        public bool? IsActive { get; set; }
    }
}
