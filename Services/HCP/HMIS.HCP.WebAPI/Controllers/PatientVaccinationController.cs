using CommonDTOs.ResponseDTO;
using HMIS.HCP.Domain.Models.DbModels;
using HMIS.HCP.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.HCP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientVaccinationController : ControllerBase
    {
        #region Class Fields & Propertities

        //private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientVaccinationService<PatientVaccination> _PatientVaccinationService;

        #endregion

        #region Constructor

        public PatientVaccinationController(TokenService tokenService, PatientVaccinationService<PatientVaccination> PatientVaccinationService)
        {
            _tokenService = tokenService;
            _PatientVaccinationService = PatientVaccinationService;
        }

        #endregion

        #region CUD Operations


        [HttpGet]
        [Route("GetPatientPreviousVaccinations")]
        public async Task<IActionResult> GetPatientPreviousVaccinations(Guid PatientId)
        {
            var response = await _PatientVaccinationService.GetPatientPreviousVaccinations(PatientId);
            return Ok(new ResponseSuccess { data = response });
        }


        #endregion
    }
}
