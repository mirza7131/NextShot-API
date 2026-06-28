using AuthDAL.Models.Dto.AttachmentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.FeatureDto
{
    public class CreateOrEditFeatureDto
    {
        public Guid? FeatureId { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public virtual ICollection<CreateOrEditAttachmentDto> Attachments
        { get; set; } = new List<CreateOrEditAttachmentDto>(); 
    }
}
