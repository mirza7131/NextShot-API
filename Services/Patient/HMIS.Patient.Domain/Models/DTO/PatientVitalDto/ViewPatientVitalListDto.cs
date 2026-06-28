using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientVitalDto
{
    public class ViewPatientVitalListDto
    {
        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public Guid? PatientVitalId { get; set; }

        public string? TokenNo { get; set; }

        public int? VisitNo { get; set; }

        public string? Mrno { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public string? Cnic { get; set; }

        public string? MobileNo { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? HealthFacilityName { get; set; }

        public DateTime? VisitDate { get; set; }

        public bool? IsDischarge { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }

    }
}
