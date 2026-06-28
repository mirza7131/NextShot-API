using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditEmcDto
    {
        public Guid? EmcId { get; set; }
        public Guid? EmcTypeProfileId { get; set; }
    }
}
