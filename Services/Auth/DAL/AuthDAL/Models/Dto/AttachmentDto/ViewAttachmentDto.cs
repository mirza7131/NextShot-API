using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.AttachmentDto
{
    public class ViewAttachmentDto
    {
        public Guid AttachmentId { get; set; }

        public Guid ParentId { get; set; }

        public string ParentType { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
