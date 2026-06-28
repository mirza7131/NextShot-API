using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.DashboardDto
{
    public class ViewPatientOpenVisitCountByGenderByMonthDto
    {
        public string? Gender { get; set; }
        public int? VisitCount { get; set; }
        public int? VisitMonth { get; set; }

    }
}
