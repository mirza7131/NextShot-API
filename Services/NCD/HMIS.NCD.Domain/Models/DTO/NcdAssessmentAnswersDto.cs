using HMIS.NCD.Domain.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class NcdAssessmentAnswersDtoForCopdAsthmaORDiabates
    {
        public Guid? NcdAssessmentAnswersId { get; set; }

        public Guid QuestionTypeProfileId { get; set; }

        public string Answer { get; set; } = null!;

        public int? Score { get; set; }
    }

    public class NcdAssessmentAnswersDto
    {
        public Guid? NcdAssessmentAnswersId { get; set; }
        public Guid QuestionTypeProfileId { get; set; }
        public string Answer { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public int? Score { get; set; }

        public List<NcdAssessmentAnswersDtoForCopdAsthmaORDiabates>? AsthmaCase { get; set; }
        public List<NcdAssessmentAnswersDtoForCopdAsthmaORDiabates>? CopdCase { get; set; }
        public List<NcdAssessmentAnswersDtoForCopdAsthmaORDiabates>? DiabatesCase { get; set; }

    }

    public class NcdAssessmentDto
    {

        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }

        public int HealthFacilityId { get; set; }
        public bool? IsNcdPositive { get; set; }
        public bool? IsMuawinPositive { get; set; }
        public List<NcdAssessmentAnswersDto> PersonalHistory { get; set; }
        public List<NcdAssessmentAnswersDto> RiskAssessmentOfMentalHealth { get; set; }
    }
    public class AssessmentDto
    {
        public string Answer { get; set; }
        public Guid NcdAssessmentAnswersId { get; set; }
        public string Question { get; set; }
        public string QuestionTypeProfileId { get; set; }
        public string ShortName { get; set; }
    }

    public class PatientAssessmentDTO
    {
        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }
        public List<NCDAssessmentQuestionsAndAnswersDto>? ncdAssessmentQuestionsAndAnswers { get; set; }
        public List<NcdRiskAssessmentDto>? ncdRiskAssessmentQuestionsAndAnswers { get; set; }

    }


    public class NcdRiskAssessmentDto
    {
        public string MentalHealthQuestion { get; set; }
        public string Answer { get; set; }
        public int? DepressoinQuestion1Score { get; set; }
        public int? DepressoinQuestion2Score { get; set; }
        public int? AnxityQuestion1Score { get; set; }
        public int? AnxityQuestion2Score { get; set; }
        public string ShortName { get; set; }
    }

    public class _PsychologicalAssessmentDTO
    {
        public string Name { get; set; }
        public string Answer { get; set; }
    }


    public class PsychologicalAssessmentDTO
    {
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? DepressionScore { get; set; }
        public int? AnxietyScore { get; set; }
        public List<_PsychologicalAssessmentDTO>? AssessmentOfDepression { get; set; }
        public List<_PsychologicalAssessmentDTO>? AssessmentOfAxniety { get; set; }
    }
    public class NCDAssessmentQuestionsAndAnswersDto
    {
        public Guid? NcdAssessmentAnswersId { get; set; }
        public Guid QuestionTypeProfileId { get; set; }
        public string Answer { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public int? Score { get; set; }
        public int? diabetesScore { get; set; }
        public int? astamaScore { get; set; }
        public int? copdScore { get; set; }

        public List<NcdAssessmentAnswersDtoForCopdAsthmaORDiabates>? AsthmaCase { get; set; }
        public List<NcdAssessmentAnswersDtoForCopdAsthmaORDiabates>? CopdCase { get; set; }
        public List<NcdAssessmentAnswersDtoForCopdAsthmaORDiabates>? DiabatesCase { get; set; }

    }



    public class NCDPatientAssessmentAnswersDTO
    {
        public PersonalHistory? personalHistory { get; set; }
        public List<ViewNcdPatientFamiliyHistory>? patientFamiliyHistory { get; set; }
    }


    public class PatientFamiliyHistoryDTO
    {
        Guid? PatientVisitId { get; set; }
        string Name { get; set; }
        string ShortName { get; set; }
        string Value { get; set; }
    }

    public class PersonalHistoryDTO
    {
        Guid? PatientId { get; set; }
        Guid? PatientVisitId { get; set; }
        int? ScoreDiabetes { get; set; }
        int? ScoreAsthama { get; set; }
        int? ScoreCopd { get; set; }
        string? KnownCaseOfHypertension { get; set; }
        string? KnownCaseOfAsthama { get; set; }
        string? KnownCaseOfDiabetes { get; set; }
        string? KnownCaseOfCopd { get; set; }
    }
}
