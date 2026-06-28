using HMIS.MIMS.Domain.Models.DTO.IndentDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO
{
    public class CreateOrEditIndentMasterDto
    {
        public CreateOrEditIndentMasterDto()
        {
            IndentDetails = new List<CreateOrEditIndentDetailDto>();
        }
        public Guid? IndentMasterId { get; set; }
        public string? IndentNumber { get; set; }
        public Guid FromMimsBranchId { get; set; }
        public Guid ToMimsBranchId { get; set; }
        public bool? IsMimsAcknowledged { get; set; }
        public int? HealthfacilityId { get; set; }
        public Guid? MimsIndentStatusProfileId { get; set; }
        public string? Remarks { get; set; }
        public Guid? MedicineSourceTypeProfileId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? IssuedOn { get; set; }
        public Guid? IssuedBy { get; set; }

        public List<CreateOrEditIndentDetailDto> IndentDetails { get; set; }

    }
}
