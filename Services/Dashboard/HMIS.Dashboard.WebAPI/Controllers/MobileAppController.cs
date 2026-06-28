using CommonDTOs.ResponseDTO;
using HMIS.Dashboard.Domain.Models.DTO.MobileAppDashboard;
using HMIS.Dashboard.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Dashboard.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    
    public class MobileAppController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly MobileAppService _mobileAppService;
        //private readonly PatientDiagnoseService<PatientDiagnose> _PatientDiagnoseService;
        private readonly string _BASBaseUrl;
        private readonly bool _isDevelopment;
        #endregion

        #region Constructor

        public MobileAppController(TokenService tokenService, MobileAppService MobileAppService, IConfiguration config)
        {
            _mobileAppService = MobileAppService;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;

        }

        #endregion

        [HttpPost]
        [Route("getHMISAllCountsForApp")]
        public async Task<IActionResult> getHMISAllCountsForApp(MobileAppDashboardDTOs filter)
        {
            var list = await _mobileAppService.getHMISAllCountsForApp(filter);
            return Ok(new ResponseSuccess { data = list });
        }


    }
}
