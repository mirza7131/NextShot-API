using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.DashboardDto
{
    public class DashboardFilterUtilityDto
    {
        public int? HealthFacilityId { get; set; }
        public int? Status { get; set; }
    }
}
