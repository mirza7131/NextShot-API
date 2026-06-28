using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO
{
    public class UpdateIndentStatusDetailDto
    {
        public Guid IndentDetailId { get; set; }
        public int? IssueQuantity { get; set; }
        public int? ReceivedQuantity { get; set; }
        public string? MedicineName { get; set; }
        public int? MedicineTypeId { get; set; }
        public int? MedicineId { get; set; }

        public string? Remarks { get; set; }

    }
    public class UpdateIndentStatusDto
    {
        public UpdateIndentStatusDto()
        {
            IndentDetails = new List<UpdateIndentStatusDetailDto>();
        }
        public Guid IndentMasterId { get; set; }
        public Guid? ToBranchId { get; set; }
        public string? IndentNumber { get; set; }
        public string? Remarks { get; set; }
        public string? StatusValue { get; set; }
        public List<UpdateIndentStatusDetailDto> IndentDetails { get; set; }
    }
}
