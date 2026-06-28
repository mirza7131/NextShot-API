using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.DashboardDto
{
    public class ViewTodayPatientVisitCountByDeptBySecDto
    {
        public int? VisitCount { get; set; }
        public int? PatientVisitRatio { get; set; }
        public string? DepartmentName { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public string? SectionName { get; set; }
        public int? HealthFacilityId { get; set; }

    }
}
