using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditBase64Dto
    {
        public Guid? ImageBaseSixtyFourId { get; set; }

        public Guid? PatientImageId { get; set; }

        public string? Base64 { get; set; }
    }
}
