using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HfLabTestConfigDto
{
    public class ViewHfLabTestConfigDto
    {
        public Guid HfLabTestConfigId { get; set; }

        public int HealthFacilityId { get; set; }

        public int LabTestId { get; set; }

        public bool? IsPerformedPrivately { get; set; }

        public Guid? LabDepartmentProfileId { get; set; }

        public bool IsActive { get; set; }
    }
}
