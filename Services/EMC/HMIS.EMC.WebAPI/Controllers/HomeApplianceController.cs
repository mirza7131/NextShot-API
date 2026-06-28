using CommonDTOs.ResponseDTO;
using HMIS.EMC.Service;
using HMIS.EMC.Service.Interfaces;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HMIS.EMC.Domain.Models.DbModels;
using Microsoft.AspNetCore.Authorization;

namespace HMIS.EMC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeDepartmentController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private  HomeApplianceService<FitnessCertificate> _homeApplianceService;
        #endregion

        #region Constructor
        public HomeDepartmentController(TokenService tokenService, HomeApplianceService<FitnessCertificate> homeApplianceService)
        {
            _tokenService = tokenService;   
            _homeApplianceService = homeApplianceService;
        }
        #endregion

        [HttpGet]
        [Route("GetSingleRecord")]
        public async Task<IActionResult> GetSingleRecord(string Cnic) 
        {
            var lst = await _homeApplianceService.Get(Cnic);
            return Ok(new ResponseSuccess{ status = true, data = lst });
        }
    }
}
