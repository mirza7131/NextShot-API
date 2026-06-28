using CommonDTOs.ResponseDTO;
using HMIS.EMC.Domain.Models.Dto;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using HMIS.EMC.Service.Interfaces;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.EMC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FitnessCertificateController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IFitnessCertificate _fitness;
        #endregion

        #region Constructor
        public FitnessCertificateController(TokenService tokenService, IFitnessCertificate fitness)
        {
            _tokenService = tokenService;
            _fitness = fitness;
        }
        #endregion

        [HttpPost]
        [Route("CreateOrEditFitnessCertificate")]
        public async Task<IActionResult> CreateOrEditFitnessCertificate(CreateOrEditFitnessCertificateDto input)
        {
            var obj = await _fitness.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpGet]
        [Route("GetAllFitnessCertificatePatients")]
        public async Task<IActionResult> GetAllFitnessCertificatePatients([FromQuery] SearchFilterDto? filter)
        {
            var lst = await _fitness.GetAllFitnessCertificatePatients(filter);
            return Ok(new ResponseSuccess { data = lst });
        }

        [HttpGet]
        [Route("GetSingleFitnessCertificatePatients")]
        public async Task<IActionResult> GetSingleFitnessCertificatePatients(Guid PatientVisitId)
        {
            var lst = await _fitness.GetSingleFitnessCertificatePatientInfo(PatientVisitId);
            return Ok(new ResponseSuccess { data = lst });
        }


        [HttpGet]
        [Route("GetTenPsychologicalAssessmentQuestions")]
        public async Task<IActionResult> GetTenPsychologicalAssessmentQuestions()
        {
            var lst = await _fitness.GetTenPsychologicalAssessmentQuestions();
            return Ok(new ResponseSuccess { data = lst });
        }
    }
}