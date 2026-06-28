using CommonDTOs.ResponseDTO;
using HMIS.NCD.Domain.Models.DTO;
using HMIS.NCD.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.NCD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssessmentQaController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly INcdAssessmentAnswers _ncdService;
        #endregion

        #region Constructor
        public AssessmentQaController(INcdAssessmentAnswers ncdService)
        {
            _ncdService = ncdService;
        }
        #endregion

        #region CU
        [HttpPost]
        [Route("CreateNcdAssessment")]
        public async Task<IActionResult> CreateOrEdit(NcdAssessmentDto input)
        {
            var obj = await _ncdService.CreateNcdAssessment(input);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpPost]
        [Route("UpdateNcdAssessment")]
        public async Task<IActionResult> UpdateNcdAssessment(NcdAssessmentDto input)
        {
            var obj = await _ncdService.UpdateNcdAssessment(input);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditNCDAssessmentQuestionAndAnswer")]
        public async Task<IActionResult> CreateOrEditNCDAssessmentQuestionAndAnswer(PatientAssessmentDTO input)
        {
            await _ncdService.CreateOrEditNCDAssessmentQuestionAndAnswer(input);
            return Ok(new ResponseSave { data = null });
        }

        [HttpPost]
        [Route("SaveNCDPsychologicalAssessment")]
        public async Task<IActionResult> SaveNCDPsychologicalAssessment(PsychologicalAssessmentDTO input)
        {
            var data = await _ncdService.SaveNCDPsychologicalAssessment(input);
            return Ok(new ResponseSave { data = data });
        }
        #endregion

        #region GET API
        [HttpGet]
        [Route("GetSingleNcdAsssessmentRecord")]
        public async Task<IActionResult> GetSingleNcdAsssessmentRecord(Guid? PatientVisitId)
        {
            var lst = await _ncdService.GetSingleNcdAsssessmentRecord(PatientVisitId);
            return Ok(new ResponseSuccess { data = lst });
        }


        [HttpGet]
        [Route("GetNcdAsssessmentAnswers")]
        public async Task<IActionResult> GetNcdAsssessmentAnswers(Guid? PatientVisitId)
        {
            var lst = await _ncdService.GetNcdAsssessmentAnswers(PatientVisitId);
            return Ok(new ResponseSuccess { data = lst });
        }

        [HttpGet]
        [Route("GetPatinetLastVisitScore")]
        public async Task<IActionResult> GetPatinetLastVisitScore(Guid? PatientVisitId)
        {
            var lst = await _ncdService.GetPatinetLastVisitScore(PatientVisitId);
            return Ok(new ResponseSuccess { data = lst });
        }

        [HttpGet]
        [Route("GetPatientLastVisitIdForNcdAssesmentQuestions")]
        public async Task<IActionResult> GetPatientLastVisitIdForNcdAssesmentQuestions(Guid PatientId, string FormType)
        {
            var lst = await _ncdService.GetPatientLastVisitIdForNcdAssesmentQuestions(PatientId, FormType);
            return Ok(new ResponseSuccess { data = lst });
        }
        #endregion
    }
}
