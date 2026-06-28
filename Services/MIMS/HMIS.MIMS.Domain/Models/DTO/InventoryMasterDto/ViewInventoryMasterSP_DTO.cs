using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.InventoryMasterDto
{
    public class ViewInventoryMasterSP_DTO
    {
        public Guid? InventoryMasterId {  get; set; }
        public int? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? AvailableQty { get; set; }
        public bool? IsSmlmedicine { get; set; }
    }
}
