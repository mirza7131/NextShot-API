using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.DashboardDto
{
    public class ViewPatientDashboardCountDto
    {
        public int? TotalPatients { get; set; }
        public int? TodayPatients { get; set; }
        public int? TotalVisits { get; set; }
        public int? TodayVisits { get; set; }
    }
}
