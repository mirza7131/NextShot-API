using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CRReports.Models.Dto
{
    public class DeathCertificateDto
    { 
        public Guid DeathCertificateId { get; set; }
        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid PatientDiagnoseId { get; set; }

        public string FullName { get; set; }
        public string HealthFacilityName { get; set; }
        public string QrCodeImagePath { get; set; }
        public string CNIC { get; set; }
        public string TrackingId { get; set; }
        public string CrmsNo { get; set; }
        public string DeceasedPersonName { get; set; }
        public string DeceasedPersonCNIC { get; set; }
        public DateTime? DeceasedPersonDob { get; set; }
        public string DeceasedPersonSicknessPeriod { get; set; }
        public DateTime? DeceasedPersonDateAndTimeOfAdmission { get; set; }
        public DateTime? DeceasedPersonDateAndTimeOfDeath { get; set; }
        public DateTime? DeceasedPersonDateOfBurlal { get; set; }
        public string DeadBodyReceivedBy { get; set; }
        public string PlaceOfDeath { get; set; }
        public string CauseOfDeath { get; set; }
        public string NatureOfDeath { get; set; }
        public string BuriedAt { get; set; }
        public string MotherName { get; set; }
        public string MotherCNIC { get; set; }
        public string FatherName { get; set; }
        public string FatherCNIC { get; set; }
        public string HusbandName { get; set; }
        public string HusbandCNIC { get; set; }
        public string Address { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantCNIC { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public string DeceasedPersonNationality { get; set; }
        public string EntryStatus { get; set; }
        public string Tehsil { get; set; }
        public string District { get; set; }
        public string Relation { get; set; }
        public string Religion { get; set; }
        public string Gender { get; set; }

    }
}