using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.FeatureDto
{
    public class ViewFeatureDto
    {
        public Guid FeatureId { get; set; }

        public string Title { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; } = null!;
    }
}
