using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{

    public class BreastCancerRiskIdentificationDTO
    {
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public string? FormType { get; set; }
        public int? DocDepartmentLookupId { get; set; }
        public int? DocSectionLookupId { get; set; }
        public string? MaritalStatus { get; set; }
        public string? IsPregnant { get; set; }
        public string? CurrentAge { get; set; }
        public int? ScoreCurrentAge { get; set; }
        public string? MenarcheAge { get; set; }
        public int? ScoreMenarche { get; set; }
        public string? FirstLiveBirthAge { get; set; }
        public int? ScoreFirstLiveBirth { get; set; }
        public string? BreastFedAge { get; set; }
        public int? ScoreBreastFed { get; set; }
        public string? Nulliparity { get; set; }
        public int? ScoreNulliparity { get; set; }
        public string? KnownFamilyBreastCancer { get; set; }
        public int? ScoreFamilyBreastCancer { get; set; }
        public string? AtypicalHyperplasia { get; set; }
        public int? ScoreAtypicalHyperplasia { get; set; }
        public string? OralHarmoneTherapy { get; set; }
        public int? ScoreHarmoneTherapy { get; set; }
        public string? OtherCancers { get; set; }
        public int? ScoreOtherCancers { get; set; }
        public string? NoBreastfed { get; set; }
        public int? ScoreTotal { get; set; }
        public string? RiskStatus { get; set; }
        public string? FNAC { get; set; }
        public CVC_AssessmentViewModel? CVC_Assessement { get; set; }
    }


}
