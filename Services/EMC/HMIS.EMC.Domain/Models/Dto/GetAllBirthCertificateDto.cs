using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class GetAllBirthCertificateDto
    {
        public string? Mrno { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public int? HealthFacilityId { get; set; }

        public DateTime? IssueDate { get; set; }

        public string? FatherName { get; set; }

        public string? ChildName { get; set; }
    }
}
