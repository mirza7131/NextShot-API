using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class MlePatientsDto
    {
        public string? Mrno { get; set; }
        public Guid Mlcid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? Address { get; set; }
        public string Cnic { get; set; } = null!;
        public int? Age { get; set; }
        public string MobileNo { get; set; } = null!;
        public Guid MlebasicInfoId { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public Guid? PatientStatusProfileId { get; set; }

        public int? HealthFacilityId { get; set; }

        public string? Mlcno { get; set; }

        public string? BookNo { get; set; }

        //public string? PatientImage { get; set; }

        public DateTime? Dob { get; set; }

        public string? Relation { get; set; }

        public string? PoliceDistrict { get; set; }
        public string? PoliceDocketOne { get; set; }

        public string? PoliceDocketTwo { get; set; }

        public string? PoliceDocketThree { get; set; }

        public DateTime? MlcDate { get; set; }
        public Guid? MLEReportId { get; set; }
        public int? RefferToHealthFacilityId { get; set; }

        public Guid? CaseTypeProfileId { get; set; }

        public Guid? DoctorId { get; set; }

        public string? IncidentPlace { get; set; }

        public string? MlcRemark1 { get; set; }

        public string? MlcRemark2 { get; set; }

        public string? AccompaniesBy { get; set; }

        public DateTime? ArrivalDateTime { get; set; }

        public DateTime? ExaminationDateTime { get; set; }

        public string? CourtOrder { get; set; }

        public string? PoliceConstableName { get; set; }

        public string? PoliceConstablePhoneNumber { get; set; }

        public DateTime? AdmitDateTime { get; set; }

        public DateTime? DischargeDateTime { get; set; }

        public string? Comments { get; set; }

        public DateTime? SentDateTime { get; set; }

        public long? ReportCounts { get; set; }

        public bool? IsFinalReport { get; set; }

        public DateTime? CreatedOn { get; set; }

        public Guid MleexaminationId { get; set; }

        public string? History { get; set; }

        public string? ClothExamination { get; set; }

        public string? GeneralPhysicalExamination { get; set; }

        public string? InjuriesDescription { get; set; }

        public string? AdvisedInvestigate { get; set; }

        public string? LaboratoryInvestigation { get; set; }

        public string? OpinionSpecialistOrXrayReport { get; set; }

        public string? NatureOfInjuries { get; set; }

        public string? Fabrication { get; set; }

        public string? DurationOfInjuries { get; set; }

        public string? WeaponPoison { get; set; }

        public string? KuoInjuries { get; set; }

        public string? GuardianName { get; set; }
        public string? GuardianCNIC { get; set; }
        public string? Caste { get; set; }
        public string? Gender { get; set; }
        public string? Occupation { get; set; }
        public string? PatientImageUrl { get; set; }
        public string? PoliceSignatureImageUrl { get; set; }
        public string? PatientSignatureImageUrl { get; set; }
        public string? PatientUnderAgeOrAdultFingerPrint { get; set; }
        //public string? PatientUnderAgeFingerPrint { get; set; }
        public string? PoliceFingerPrint { get; set; }
        public string? PatientManualReport { get; set; }
        public string? PatientDrawImgUrl { get; set; }
        public Guid? PatientImageId { get; set; }
        public Guid? PoliceSignatureImageId { get; set; }
        public Guid? PatientSignatureImageId { get; set; }
        public Guid? AdultOrUnderAgeFingerPrintProfileId { get; set; }
        //public Guid? PatientUnderAgeFingerPrintId { get; set; }
        public Guid? PoliceFingerPrintId { get; set; }
        public Guid? PatientManualReportId { get; set; }
        public Guid? PatientDrawImgId { get; set; }
    }
}
