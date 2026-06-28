using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditBirthCertificateDto
    {
        public Guid? BirthCertificateId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }
        public Guid? DoctorId { get; set; }

        public int? HealthFacilityId { get; set; }

        public DateTime? IssueDate { get; set; }

        public string? MotherName { get; set; }

        public string? MotherCnic { get; set; }

        public string? FatherName { get; set; }
        public string? FormType { get; set; }

        public int? DocDepartmentLookupId { get; set; }

        public int? DocSectionLookupId { get; set; }

        public string? FatherCnic { get; set; }

        public Guid? OccupationTypeProfileId { get; set; }

        public Guid? FatherNationalityTypeProfileId { get; set; }

        public Guid? MotherNationalityTypeProfileId { get; set; }

        public Guid? ReligiousProfileId { get; set; }

        public string? GrandFatherName { get; set; }

        public string? ChildName { get; set; }
        public string? FullName { get; set; }
        public string? HealthFacilityName { get; set; }

        public Guid? GenderProfileId { get; set; }

        public Guid? ChildBloodGroupProfileId { get; set; }

        public Guid? MotherBloodGroupProfileId { get; set; }

        public int? AnnualNumber { get; set; }

        public DateTime? Dob { get; set; }

        public string? PlaceOfBirth { get; set; }


        public DateTime? EntryDate { get; set; }

        public Guid? EntryStatusProfileId { get; set; }
        public string? GynaeUnit { get; set; }

        public int? DistrictOfBirthId { get; set; }
        public int? DistrictId { get; set; }

        public string? GrandFatherCnic { get; set; }

        public string? Address { get; set; }

        public int? TehsilId { get; set; }
        public CreateOrEditEmcDto? emc { get; set; }
    }
}
