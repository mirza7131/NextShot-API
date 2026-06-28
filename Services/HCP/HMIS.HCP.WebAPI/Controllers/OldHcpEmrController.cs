using CommonDTOs.ResponseDTO;
using HMIS.HCP.Domain.Models.OldDbModels;
using HMIS.HCP.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.HCP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldHcpEmrController : ControllerBase
    {
        #region Class Fields & Propertities

        //private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly OldHcpEmrService<TblLabSample> _OldHcpEmrService;

        #endregion

        #region Constructor

        public OldHcpEmrController(TokenService tokenService, OldHcpEmrService<TblLabSample> OldHcpEmrController)
        {
            _tokenService = tokenService;
            _OldHcpEmrService = OldHcpEmrController;
        }

        #endregion


        #region APIs
        [HttpGet]
        [Route("GetTotalSamples")]
        public async Task<IActionResult> GetTotalSamples(string PatientCnic)
        {
            var response = await _OldHcpEmrService.GetTotalSamples(PatientCnic);
            return Ok(new ResponseSuccess { data = response });
        }
        [HttpGet("GetTotalSamplesWithCnic")]
        public async Task<IActionResult> GetTotalSamplesWithCnic(string PatientCnic)
        {
            var response = await _OldHcpEmrService.GetTotalSamplesWithCnic(PatientCnic);
            return Ok(new ResponseSuccess { data = response });
            //return null;
        }
        #endregion
    }
}
