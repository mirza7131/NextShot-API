using HMIS.NCD.Domain.Models.DbModels;
using HMIS.NCD.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Service.Interfaces
{
    public interface INcdAssessmentAnswers
    {
        Task<NcdAssessmentDto> CreateNcdAssessment(NcdAssessmentDto input);
        Task<NcdAssessmentDto> UpdateNcdAssessment(NcdAssessmentDto input);
        Task<List<SingleNcdAsssessmentRecord>> GetSingleNcdAsssessmentRecord(Guid? PatientVisitId);
        public Task CreateOrEditNCDAssessmentQuestionAndAnswer(PatientAssessmentDTO assessmentDTO);
        public Task<NCDPatientAssessmentAnswersDTO> GetNcdAsssessmentAnswers(Guid? PatientVisitId);
        public Task<MentalHealthPatientDetail> SaveNCDPsychologicalAssessment(PsychologicalAssessmentDTO assessmentDTO);
        public Task<dynamic> GetPatientLastVisitIdForNcdAssesmentQuestions(Guid PatientId, string FormType);
        public Task<MentalHealthPatientDetail> GetPatinetLastVisitScore(Guid? PatientVisitId);
    }
}
