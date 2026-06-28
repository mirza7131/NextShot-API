using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CRReports.Models.Dto
{
    public class FitnessCertificateDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string CNIC { get; set; }
        public string ParmanentAddress { get; set; }
        public string HealthFacilityName { get; set; }
        public string QrCodeImagePath { get; set; }
        public string Education { get; set; }
        public string HivTest { get; set; }
        public string Profession { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? CreatedOn { get; set; }

        // Fitness Certificate Information
        public string TrackingId { get; set; }
        public string RelativeName { get; set; }
        public string EmployeeLetterNo { get; set; }
        public DateTime? LetterDateTime { get; set; }
        public string DesignationAppliedFor { get; set; }
        public int AgeByAppearance { get; set; }
        public string CheckedBy { get; set; }
        public DateTime? IssueDate { get; set; }
        public string BodilyInfirmity { get; set; }
        public string Department { get; set; }

        // CBC Information
        public string HB { get; set; }
        public string MCV { get; set; }
        public string HCV { get; set; }
        public decimal TLC { get; set; }
        public decimal Neutorphils { get; set; }
        public decimal Lymphocytes { get; set; }
        public decimal Eosinophils { get; set; }
        public decimal Platelets { get; set; }
        public decimal ESR { get; set; }
        public decimal Monocytes { get; set; }

        // General Parameter Information
        public string Vision { get; set; }
        public string RecomendedOrNot { get; set; }
        public decimal Chest { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal BpDiaSystolic { get; set; }
        public decimal BpSystolic { get; set; }
        public string MarkOfIdentification { get; set; }
        public string BSR { get; set; } = string.Empty;
        public string Remarks { get; set; }
        public string PresentJob { get; set; }
        public decimal SalmonellaTyphiO { get; set; }
        public decimal SalmonellaTyphiH { get; set; }
        public decimal SalmonellaTyphiAO { get; set; }
        public decimal SalmonellaTyphiAH { get; set; }
        public decimal SalmonellaTyphiBO { get; set; }
        public decimal SalmonellaTyphiBH { get; set; }
        public bool IsWidal { get; set; }

        // Serology Test Information
        public string SerologyTestOne { get; set; }
        public string SerologyTestTwo { get; set; }

        // Urine CE Information
        public string Color { get; set; }
        public decimal SpecificGravity { get; set; }
        public decimal PH { get; set; }
        public decimal Protien { get; set; }
        public decimal Glucose { get; set; }
        public decimal Ketones { get; set; }
        public decimal Urobilinogen { get; set; }
        public decimal PussCells { get; set; }
        public decimal RBCs { get; set; }
        public decimal Crystals { get; set; }
        public decimal EpethlialCells { get; set; }
        public decimal Bacteria { get; set; }
        public decimal Casts { get; set; }

        // Image URLs
        public string PatientImageUrl { get; set; }
        public string PatientRightThumbImageUrl { get; set; }
        public string PatientRightIndexImageUrl { get; set; }
        public string PatientRightMiddleImageUrl { get; set; }
        public string PatientRightRingImageUrl { get; set; }
        public string PatientRightLittleImageUrl { get; set; }
        public string Colour { get; set; }
        public string Consistency { get; set; }
        public decimal Mucus { get; set; }
        public decimal Blood { get; set; }
        public decimal FitnessStoolExaminationPussCells { get; set; }
        public decimal OVA { get; set; }
        public decimal FitnessStoolExaminationRBCs { get; set; }
        public decimal VegetativeForms { get; set; }
        public decimal OccultBlood { get; set; }
        public string XRayChestPAView { get; set; }

        // User Information
        public string UserName { get; set; }

        // Additional Profile Information
        public string SerologyTest1Value { get; set; }
        public string SerologyTest2Value { get; set; }
        public string PregNancyTest { get; set; }
        public string HepBTest { get; set; }
        public string HepCTest { get; set; }
    }
    public class PsychologyDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class GetSingleFitnessCertificateDtoWithQuestions
    {
        public List<FitnessCertificateDto> fitnessSingleRecord { get; set; }
        public List<PsychologyDto> PsychologicalQuestionsAndAnswer { get; set; }
    }
}