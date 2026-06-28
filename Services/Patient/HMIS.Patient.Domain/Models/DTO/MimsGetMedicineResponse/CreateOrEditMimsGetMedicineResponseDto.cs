using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MimsGetMedicineResponse
{
    public class CreateOrEditMimsGetMedicineResponseDto
    {
        public Guid MimsGetMedicineResponseId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? ResponseData { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid? CreatedBy { get; set; }

        public long? MimsIndentId { get; set; }

        public int? WardId { get; set; }
        public string? WardName { get; set; }
        
        public bool? SyncStatus { get; set; }
        public bool? IsActive { get; set; }
        public byte ActionTypeId { get; set; }

    }

}
