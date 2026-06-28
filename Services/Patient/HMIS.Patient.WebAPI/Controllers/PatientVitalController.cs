using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using HMIS.Patient.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DbModel = HMIS.Patient.Domain.Models.DbModels;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientVitalController : ControllerBase
    {

        #region Class Fields & Propertities

        private readonly ILogger<PatientVitalController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientVitalService<PatientVital> _PatientVitalService;

        #endregion

        #region Constructor

        public PatientVitalController(ILogger<PatientVitalController> logger, TokenService tokenService, PatientVitalService<DbModel.PatientVital> PatientVitalService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _PatientVitalService = PatientVitalService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientVitalDto input)
        {
            var obj = await _PatientVitalService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditPatientVitals")]
        public async Task<IActionResult> CreateOrEditPatientVitals(CreateOrEditPatientVitalDto input)
        {
            var obj = await _PatientVitalService.CreateOrEditPatientVitals(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _PatientVitalService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _PatientVitalService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllPatientVitalsList")]
        public async Task<IActionResult> GetAllPatientVitalsList([FromQuery] FilterPatientVitalDto filter)
        {
            var list = await _PatientVitalService.GetAllPatientVitalsList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetRefferedPatientVitalsList")]
        public async Task<IActionResult> GetRefferedPatientVitalsList([FromQuery] FilterPatientVitalDto filter)
        {
            var list = await _PatientVitalService.GetRefferedPatientVitalsList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _PatientVitalService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetAllQue")]
        public async Task<IActionResult> GetAllQue(int? HealthFacilityId)
        {
            var list = await _PatientVitalService.GetAllQue(HealthFacilityId);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("GetByVisitId")]
        public async Task<IActionResult> GetByVisitId(Guid VisitId)
        {
            var list = await _PatientVitalService.GetByVisitId(VisitId);
            return Ok(new ResponseSuccess { data = list });
        }
        

        [HttpGet]
        [Route("GetVitalsByVisitId")]
        public async Task<IActionResult> GetVitalsByVisitId(Guid VisitId)
        {
            var list = await _PatientVitalService.GetVitalsByVisitId(VisitId);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region Helper Methods


        #endregion

    }
}
