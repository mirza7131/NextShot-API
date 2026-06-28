using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class GetAllFitnessCertificateDto
    {
        public string? EmployeeLetterNo { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid PatientDiagnoseId { get; set; }
        public int HealthFacilityId { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public string Cnic { get; set; } = null!;
        public string Tehsil { get; set; } = null!;
        public DateTime? IssueDate { get; set; }

        public string? DesignationAppliedFor { get; set; }
    }
}
