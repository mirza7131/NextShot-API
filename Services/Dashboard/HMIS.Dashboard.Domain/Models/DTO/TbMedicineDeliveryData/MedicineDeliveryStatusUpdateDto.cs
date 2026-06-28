using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.TbMedicineDeliveryData
{
    public class MedicineDeliveryStatusUpdateDto
    {
        public int Id { get; set; }
        public int DeliveryStatus { get; set; }
    }
}
