using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class GetSingleFitnessCertificateDto
    {
        public Guid? FitnessCertificateId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public int? HealthFacilityId { get; set; }

        public Guid? PatientImageId { get; set; }

        public Guid? RightThumbImageId { get; set; }

        public Guid? RightIndexImageId { get; set; }

        public Guid? RightMiddleImageId { get; set; }
        public Guid? RecomendedOrnotTypeProfileId { get; set; }

        public Guid? RightRingImageId { get; set; }

        public Guid? RightLittleImageId { get; set; }

        public Guid? PreparedByUserId { get; set; }
        public Guid? EducationTypeProfileId { get; set; }
        public Guid? ProfessionTypeId { get; set; }
        public string? RelativeName { get; set; }
        public string? EmployeeLetterNo { get; set; }
        public string? UserName { get; set; }
        public string? PresentJob { get; set; }
        public decimal? BpSystolic { get; set; }

        public decimal? BpDiaSystolic { get; set; }
        public DateTime? LetterDateTime { get; set; }

        public string? DesignationAppliedFor { get; set; }
        public Guid? DesignationAppliedForTypeProfileId { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? FullName { get; set; }
        public string? ShortName { get; set; }

        public int? AgeByAppearance { get; set; }

        public string? CheckedBy { get; set; }

        public DateTime? IssueDate { get; set; }

        public string? BodilyInfirmity { get; set; }

        public string? Department { get; set; }

        // cbc
        public Guid? FitnessCbcid { get; set; }

        public string? Hb { get; set; }

        public string? Mcv { get; set; }

        public string? Hcv { get; set; }

        public decimal? Tlc { get; set; }

        public decimal? Neutorphils { get; set; }

        public decimal? Lymphocytes { get; set; }

        public decimal? Eosinophils { get; set; }

        public decimal? Platelets { get; set; }

        public decimal? Esr { get; set; }

        public decimal? Monocytes { get; set; }

        // widal
        public Guid? FitnessGeneralParameterId { get; set; }

        public string? Vision { get; set; }

        public decimal? Chest { get; set; }

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public string? MarkOfIdentification { get; set; }

        public Guid? PregnancyTestProfileId { get; set; }

        public string? Bsr { get; set; }

        public Guid? HivtestProfileId { get; set; }

        public Guid? HepBtestProfileId { get; set; }

        public Guid? HepCtestProfileId { get; set; }

        public string? Remarks { get; set; }

        public decimal? SalmonellaTyphiO { get; set; }

        public decimal? SalmonellaTyphiH { get; set; }

        public decimal? SalmonellaTyphiAo { get; set; }

        public decimal? SalmonellaTyphiAh { get; set; }

        public decimal? SalmonellaTyphiBo { get; set; }

        public decimal? SalmonellaTyphiBh { get; set; }

        public bool? IsWidal { get; set; }

        // Fitness Serology
        public Guid? FitnessSerologyId { get; set; }

        public string? SerologyTestOne { get; set; }

        public Guid? SerologyTestOneValueTypeProfileId { get; set; }

        public string? SerologyTestTwo { get; set; }

        public Guid? SerologyTestTwoValueTypeProfileId { get; set; }

        // Futness Urine CE
        public Guid? FitnessUrineCeid { get; set; }

        public string? Color { get; set; }

        public decimal? SpecificGravity { get; set; }

        public decimal? Ph { get; set; }

        public decimal? Protien { get; set; }

        public decimal? Glucose { get; set; }

        public decimal? Ketones { get; set; }

        public decimal? Urobilinogen { get; set; }

        public decimal? PussCells { get; set; }

        public decimal? Rbcs { get; set; }

        public decimal? Crystals { get; set; }

        public decimal? EpethlialCells { get; set; }

        public decimal? Bacteria { get; set; }

        public decimal? Casts { get; set; }
        // Stool Examination

        public Guid FitnessStoolExaminationId { get; set; }

        public string? Colour { get; set; }

        public string? Consistency { get; set; }

        public decimal? Mucus { get; set; }

        public decimal? Blood { get; set; }

        public decimal? FitnessStoolExaminationPussCells { get; set; }

        public decimal? Ova { get; set; }

        public decimal? FitnessStoolExaminationRBCs { get; set; }

        public decimal? VegetativeForms { get; set; }

        public decimal? OccultBlood { get; set; }

        public string? XrayChestPaview { get; set; }

        // Image Urls
        public string? PatientImageUrl { get; set; }
        public string? PatientRightThumbImageUrl { get; set; }
        public string? PatientRightIndexImageUrl { get; set; }
        public string? PatientRightMiddleImageUrl { get; set; }
        public string? PatientRightRingImageUrl { get; set; }
        public string? PatientRightLittleImageUrl { get; set; }
    }

    public class PsychologicalQuestionAndAnswers
    {
        public Guid? ProfileId { get; set; }
        public Guid MentalAssessmentId { get; set; }
        public string? Answer { get; set; }
        public string? Question { get; set; }
        public string? Option1 { get; set; }

        public string? Option2 { get; set; }

        public string? Option3 { get; set; }

        public string? Option4 { get; set; }

        public string? Option5 { get; set; }

        public string? Option6 { get; set; }

        public string? Option7 { get; set; }

        public string? Option8 { get; set; }

        public string? Option9 { get; set; }
    }

    public class GetSingleFitnessCertificateDtoWithQuestions
    {
        public List<GetSingleFitnessCertificateDto>? fitnessSingleRecord { get; set; }
        public List<PsychologicalQuestionAndAnswers>? PsychologicalQuestionsAndAnswer { get; set; }
    }
}
