using CommonDTOs.ResponseDTO;
using HMIS.HCP.Domain.Models.DbModels;
using HMIS.HCP.Domain.Models.DTO;
using HMIS.HCP.Service;
using HMIS.Patient.Domain.Models.DTO.NewFolder;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.HCP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientScreeningController : ControllerBase
    {
        #region Class Fields & Propertities

        //private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientScreeningService<PatientScreening> _PatientScreeningService;

        #endregion

        #region Constructor

        public PatientScreeningController(TokenService tokenService, PatientScreeningService<PatientScreening> PatientScreeningService)
        {
            _tokenService = tokenService;
            _PatientScreeningService = PatientScreeningService;
        }

        #endregion

        #region CUD Operations
        //public int MyProperty { get; set; }
        //public int asd { get; set; }
        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientScreeningDto input)
        {
            var obj = await _PatientScreeningService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }
        
        [HttpPost]
        [Route("CreateOrEditCallDetail")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditCallDto input)
        {
            var obj = await _PatientScreeningService.CreateOrEditCallDetail(input);
            return Ok(new ResponseSave { data = obj });
        }


        [HttpGet]
        [Route("GetPatientPreviousScreening")]
        public async Task<IActionResult> GetPatientPreviousScreening(Guid PatientId)
        {
            var response = await _PatientScreeningService.GetPatientPreviousScreening(PatientId);
            return Ok(new ResponseSuccess { data = response });
        }
        
        [HttpGet]
        [Route("GetSinglePatientCallDetailRecord")]
        public async Task<IActionResult> GetSinglePatientCallDetailRecord(Guid PatientId)
        {
            var response = await _PatientScreeningService.GetSinglePatientCallDetailRecord(PatientId);
            return Ok(new ResponseSuccess { data = response });
        }
        
        [HttpGet]
        [Route("SpGetSinglePatientCallDetail")]
        public async Task<IActionResult> SpGetSinglePatientCallDetail(Guid PatientId)
        {
            var response = await _PatientScreeningService.SpGetSinglePatientCallDetail(PatientId);
            return Ok(new ResponseSuccess { data = response });
        }
        //public int MyProperty { get; set; }
        //public int asd { get; set; }


        #endregion
    }
}
