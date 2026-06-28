using HMIS.Aggregator.API.Models.MIMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.MimsMedicineData
{
    public class ViewMimsMedicineDataDto
    {
        public Guid MimsMedicineDataId { get; set; }
        public int? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public int? MedicineTypeId { get; set; }
        public string? MedicineTypeName { get; set; }
        public int? WardId { get; set; }
        public string? WardName { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? AvailableQuantity { get; set; }
        public decimal? TotalDispatchQuantity { get; set; }
        public int? HealthFacilityId { get; set; }
        public decimal? UnitPrice { get; set; }

        public bool? IsSMLMedicine { get; set; }
        public class ResponseMedicineAvailableDto
        {
            public int Count { get; set; } = 0;
            public string? Message { get; set; }
            public bool Success { get; set; }
            public List<ViewMimsMedicineDataDto> Data { get; set; }

        }
    }

}
