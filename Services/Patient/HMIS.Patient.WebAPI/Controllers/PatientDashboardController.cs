using CommonDTOs.Enums;
using CommonDTOs.FilterDTO;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using HMIS.Patient.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientDashboardController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientVitalController> _logger;
        private readonly TokenService _tokenService;
        private readonly DashboardService _DashboardService;
        #endregion

        #region Constructor

        public PatientDashboardController(ILogger<PatientVitalController> logger, TokenService tokenService, DashboardService DashboardService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _DashboardService = DashboardService;
        }

        #endregion

        #region CUD Operations

        #endregion

        #region Read Operations

        //[HttpGet]
        //[Route("GetDashboardCounts")]
        ////public async Task<IActionResult> GetDashboardCounts(FilterDTO? filter)
        //public async Task<IActionResult> GetDashboardCounts()
        //{
        //    var list = await _DashboardService.GetDashboardCounts();
        //    return Ok(new ResponseSuccess { data = list });
        //}

        #endregion

        #region Helper Methods


        #endregion
    }
}
