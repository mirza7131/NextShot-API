using HMIS.Pathalogy.Domain.Models.DbModels;
using HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDto
{
    public class CreateOrEditSampleConsignmentDto
    {
        public Guid? SampleConsignmentId { get; set; }
        public string? Title { get; set; }
        public int? ToHealthFacilityId { get; set; }
        public int? FromHealthFacilityId { get; set; }
        public string? BatchNo { get; set; }
        public byte? Status { get; set; }
        public virtual ICollection<CreateOrEditSampleConsignmentDetailDto> SampleConsignmentDetails { get; set; } = new List<CreateOrEditSampleConsignmentDetailDto>();
    }
}
