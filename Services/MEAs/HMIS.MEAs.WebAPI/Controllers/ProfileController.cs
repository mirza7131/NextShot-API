using HMIS.MEAs.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;




using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HMIS.MIMS.Domain.Models.DbModels;
using CommonDTOs.ResponseDTO;
using System.Reflection.Metadata;
using HMIS.MEAs.Domain.Models.DTO.ProfileModel;

namespace HMIS.MEAs.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly ProfileServices _profile;
        //  private readonly TokenService _tokenService;

        #region Constructor

        public ProfileController(ProfileServices profile)
        {

            //_tokenService = tokenService;
            _profile = profile;

        }


        #endregion


        [HttpGet]
        [Route("GetHealthFacilitiesZone")]
        public async Task<IActionResult> GetUsersVisits()
        {
            var data = await _profile.GetHealthFacilitiesZone();
            return Ok(new ResponseSuccess { data = data });
        }


        [HttpGet]
        [Route("IndicatorDetailData")]
        public async Task<IActionResult> IndicatorDetailData()
        {
            var data = await _profile.IndicatorDetailData();
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("GetHealthFacilitiesByDistrict")]
        public async Task<IActionResult> GetHealthFacilitiesByDistrict(string DistrictCode)
        {
            var data = await _profile.GetHealthFacilitiesByDistrict(DistrictCode);
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("SavePackagesDetail")]
        public async Task<IActionResult> SavePackagesDetail(BundalDetailDTO bundle)
        {
            var data = await _profile.SavePackagesDetail(bundle);
            return Ok(new ResponseSuccess { data = data });
        }

    }
}
