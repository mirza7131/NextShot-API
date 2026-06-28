using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditIcvCertificateDto
    {
        public Guid? IcvCertificateId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? DoctorId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? OpdNo { get; set; }

        public DateTime? IssueDate { get; set; }

        public string? Name { get; set; }
        public string? HealthFacilityName { get; set; }

        public string? RelativeName { get; set; }

        public string? Cnic { get; set; }

        public Guid? NationalityTypeProfileId { get; set; }

        public string? PassportNo { get; set; }

        public string? Condition { get; set; }

        public string? Vaccination { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? NameOfDisease { get; set; }

        public DateTime? VaccinationDate { get; set; }
        public string? FormType { get; set; }

        public int? DocDepartmentLookupId { get; set; }

        public int? DocSectionLookupId { get; set; }
        public CreateOrEditEmcDto? emc { get; set; }
    }
}
