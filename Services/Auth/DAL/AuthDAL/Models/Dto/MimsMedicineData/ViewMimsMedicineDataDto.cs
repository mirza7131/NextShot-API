using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.MimsMedicineData
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

        public decimal? AvailableQuantity { get; set; }

    }

}
