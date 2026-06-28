using HMIS.Pathalogy.Domain.Models.DTO.ProfileTypeDto;
using HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDto
{
    public class ViewSampleConsignmentDto
    {
        public Guid? SampleConsignmentId { get; set; }
        public string? Title { get; set; }
        public int? ToHealthFacility { get; set; }
        public int? FromHealthFacility { get; set; }
        public string? BatchNo { get; set; }
        public byte? Status { get; set; }
        public virtual ICollection<ViewSampleConsignmentDetailDto> SampleConsignmentDetails { get; } = new List<ViewSampleConsignmentDetailDto>();


    }
}
