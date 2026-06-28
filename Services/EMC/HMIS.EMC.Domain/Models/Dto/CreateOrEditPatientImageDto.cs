using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditPatientImageDto
    {
        public Guid? PatientImageId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? ImageTypeProfileId { get; set; }

        //public Guid? UnknownPatientId { get; set; }
        public Guid? ImageBaseSixtyFourId { get; set; }
        public string? base64 { get; set; }

        public Guid? ProfileTypeId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
