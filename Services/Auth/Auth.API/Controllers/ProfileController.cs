using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.ProfileDto;
using AuthDAL.Models.Dto.ProfileTypeDto;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProfileController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly ProfileService<Profile> _ProfileService;

        #endregion

        #region Constructor

        public ProfileController(ILogger<AuthenticationController> logger, TokenService tokenService, ProfileService<Profile> ProfileService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _ProfileService = ProfileService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditProfileDto input)
        {
            var obj = await _ProfileService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _ProfileService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ProfileService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterProfileDto profileDto)
        {
            var response = await _ProfileService.GetAllWithPagination(profileDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetAllWithProfileType")]
        public async Task<IActionResult> GetAllWithProfileType([FromQuery]FilterProfileDto profileDto)
        {
            var response = await _ProfileService.GetAllWithProfileType(profileDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _ProfileService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetProfileByProfileType")]
        public async Task<IActionResult> GetProfileByProfileType(string ProfileType)
        {
            var obj = await _ProfileService.GetProfileByProfileType(ProfileType);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetProfileByShortName")]
        public async Task<IActionResult> GetProfileByShortName(string ShortName)
        {
            var obj = await _ProfileService.GetProfileByShortName(ShortName);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetDataByProfile")]
        public async Task<IActionResult> GetDataByProfile(string ProfileType)
        {
            var obj = await _ProfileService.GetDataByProfile(ProfileType);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
