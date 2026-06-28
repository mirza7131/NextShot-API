using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.StockCheckDto
{
    public class StockCheckResponseDto
    {
        public Guid InventoryMasterId { get; set; }

        public int? MedicineId { get; set; }

        public string? MedicineName { get; set; }

        public int? MedicineTypeId { get; set; }

        public Guid? MimsBranchId { get; set; }

        public string? BranchName { get; set; }

        public int? HealthfacilityId { get; set; }

        public decimal? TotalQty { get; set; }

        public decimal? IssuedQty { get; set; }

        public decimal? LockedQty { get; set; }

        public decimal? AvailableQty { get; set; }

        public decimal? CurrentUnitPrice { get; set; }

        public decimal? AvgUnitPrice { get; set; }

        public bool? IsSmlmedicine { get; set; }

        public bool? IsActive { get; set; }
    }
}
