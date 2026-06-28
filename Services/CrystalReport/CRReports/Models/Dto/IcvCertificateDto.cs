using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CRReports.Models.Dto
{
    public class IcvCertificateDto
    {
        public Guid IcvCertificateId { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid PatientDiagnoseId { get; set; }
        public int HealthFacilityId { get; set; }
        public string OpdNo { get; set; }
        public DateTime? IssueDate { get; set; }
        public string RelativeName { get; set; }
        public string QrCodeImagePath { get; set; }
        public string Nationality { get; set; }
        public string PassportNo { get; set; }
        public string Condition { get; set; }
        public string HealthFacility { get; set; }
        public string Vaccination { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NameOfDisease { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string FullName { get; set; }
        public string CNIC { get; set; }
        public string Relation { get; set; }
        public int Age { get; set; }
    }

    public class IcvVacination
    {
        public string name { get; set; }
    }
}