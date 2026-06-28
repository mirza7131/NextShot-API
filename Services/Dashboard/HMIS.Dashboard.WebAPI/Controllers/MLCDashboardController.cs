using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Services;
using HMIS.Dashboard.Domain.Models.DbModels;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto;
using HMIS.Dashboard.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Dashboard.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Dashboard.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MLCDashboardController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly MLCDashboardService _MLCDashboardService;
        private readonly bool _isDevelopment;
        #endregion

        #region Constructor

        public MLCDashboardController(TokenService tokenService, MLCDashboardService MLCDashboardService)
        {
            _tokenService = tokenService;
            _MLCDashboardService = MLCDashboardService;
        }

        #endregion

        #region MLC Dashboard Count
        [HttpGet]
        [Route("getMedicoLegalDashboardAllCounts")]
        public async Task<IActionResult> getMedicoLegalDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _MLCDashboardService.getMedicoLegalDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion
        #region EMC Dashboard Counts
        [HttpGet]
        [Route("getEMCDashboardAllCounts")]
        public async Task<IActionResult> getEMCDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _MLCDashboardService.getEMCDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

    }
}
