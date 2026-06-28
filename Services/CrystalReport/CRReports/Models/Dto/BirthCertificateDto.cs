using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CRReports.Models.Dto
{
    public class BirthCertificateDto
    {
        public Guid BirthCertificateId { get; set; }

        public string Mrno { get; set; }
        public string HealthFacilityName { get; set; }
        public string QrCodeImagePath { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid PatientDiagnoseId { get; set; }

        public int HealthFacilityId { get; set; }

        public DateTime? IssueDate { get; set; }

        public string MotherName { get; set; }

        public string MotherCnic { get; set; }

        public string FatherName { get; set; }

        public string FatherCnic { get; set; }

        public Guid OccupationTypeProfileId { get; set; }

        public Guid FatherNationalityTypeProfileId { get; set; }

        public Guid MotherNationalityTypeProfileId { get; set; }

        public Guid ReligiousProfileId { get; set; }

        public string GrandFatherName { get; set; }

        public string ChildName { get; set; }

        public Guid GenderProfileId { get; set; }

        public Guid ChildBloodGroupProfileId { get; set; }

        public Guid MotherBloodGroupProfileId { get; set; }

        public int AnnualNumber { get; set; }

        public DateTime? Dob { get; set; }

        public string PlaceOfBirth { get; set; }

        public string GynaeUnit { get; set; }
        public string Religion { get; set; }
        public string ChildGender { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string MotherNationality { get; set; }

        public string FatherNationality { get; set; }

        public string ChildBloodGroup { get; set; }

        public string MotherBloodGroup { get; set; }
        public string Occupation { get; set; }
        public int? DistrictId { get; set; }

        public DateTime? EntryDate { get; set; }

        public Guid EntryStatusProfileId { get; set; }
        public string GrandFatherCnic { get; set; }
        public string EntryStatus { get; set; }
        public string ApplicantCnic { get; set; }
        public string ApplicantName { get; set; }
        public string Relation { get; set; }
        public string ParmanentAddress { get; set; }
        public string TemporaryAddress { get; set; }
        public string Tehsil { get; set; }
        public string District { get; set; }
        public string Gender { get; set; }
        public string TrackingId { get; set; }
        public string DistrictOfBirth { get; set; }
        public string CrmsNo { get; set; }
    }
}