using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.InventoryDetailDto
{
    public class ViewInventoryDetailSP_DTO
    {
        public Guid? InventoryMasterId { get; set; }
        public string? MedicineName { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? ExpDate { get; set; }
        public decimal? AvailableQty { get; set; }
        public bool? IsSmlmedicine { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
