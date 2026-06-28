using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class FitnessCertificateHomeAppliance
    {
        public string? RelativeName { get; set; }
        public string? EmployeeLetterNo { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? FullName { get; set; }
        public DateTime? LetterDateTime { get; set; }
        public string? DesignationAppliedFor { get; set; }
        public int? AgeByAppearance { get; set; }
        public string? CheckedBy { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? BodilyInfirmity { get; set; }
        public string? Department { get; set; }
        public string? Profession { get; set; }
        public string? Education { get; set; }
        public string? RecomendationStatus { get; set; }


        public string? Vision { get; set; }

        public decimal? Chest { get; set; }

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public string? MarkOfIdentification { get; set; }

        public string? PatientImageUrl { get; set; }
        public string? PatientRightThumbImageUrl { get; set; }
        public string? PatientRightIndexImageUrl { get; set; }
        public string? PatientRightMiddleImageUrl { get; set; }
        public string? PatientRightRingImageUrl { get; set; }
        public string? PatientRightLittleImageUrl { get; set; }
        public string? QrCodeImagePath { get; set; }
        public string? PregNancyTest { get; set; }

        public string? Bsr { get; set; }
        public string? HivTest { get; set; }
        public string? HepCTest { get; set; }
        public string? HepBTest { get; set; }
        public decimal? BpSystolic { get; set; }

        public decimal? BpDiaSystolic { get; set; }
        public string? SerologyTestOne { get; set; }

        public string? SerologyTestTwo { get; set; }

    }

    public class PsychologyDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class GetSingleFitnessCertificateDtoWithQuestion
    {
        public FitnessCertificateHomeAppliance? fitnessRecord { get; set; }
        public List<PsychologyDto>? PsychologicalQuestionsAndAnswer { get; set; }
    }
}
