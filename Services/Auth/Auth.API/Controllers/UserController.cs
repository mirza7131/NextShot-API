using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.ProfileDto;
using AuthDAL.Models.Dto.UserDto;
using Azure;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HealthFacilityBAL;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using UserBAL;
using HMIS.Aggregator.API.Services;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly UserService<User> _UserService;
        private readonly HRService _HRService;

        #endregion

        #region Constructor

        public UserController(ILogger<AuthenticationController> logger, TokenService tokenService, UserService<User> UserService, HRService hrService)
        {   _logger = logger;
            _tokenService = tokenService;
            _UserService = UserService;
            _HRService = hrService;
        }

        #endregion

        #region CUD Operations


        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditUserDto input)
        {
            var obj = await _UserService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditHrUser")]
        public async Task<IActionResult> CreateOrEditHrUser(CreateOrEditHrUserDto input)
        {
            var obj = await _UserService.CreateOrEditHrUser(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto input)
        {
            var obj = await _UserService.UpdatePassword(input);
            return Ok(new ResponseSave { data = obj });
        }


        [HttpPost]
        [Route("CreateUserAssignableRoles")]
        public async Task<IActionResult> CreateUserAssignableRoles(CreateOrEditUserAssignableRolesDTO input)
        {
            var obj = await _UserService.CreateUserAssignableRoles(input);
            return Ok(new ResponseSave { data = obj });
        }



        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _UserService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _UserService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetUserAssignableRoles")]
        public async Task<IActionResult> GetUserAssignableRolesById(Guid input)
        {
            var obj = await _UserService.GetUserAssignableRolesById(input);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllByUserLevelwise")]
        public async Task<IActionResult> GetAllByUserLevelwise(string RoleConst, int? HealthFacilityId, int? DepartmentLookupId, int? SectionLookupId)
        {
            var response = await _UserService.GetAllByUserLevelwise(RoleConst, HealthFacilityId, DepartmentLookupId, SectionLookupId);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterUserDto filterUserDto)
        {
            var response = await _UserService.GetAllWithPagination(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _UserService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetByCnic")]
        public async Task<IActionResult> GetByCnic(string Cnic)
        {
            var obj = await _UserService.GetByCnic(Cnic);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetByIdWithUserRoleDetails")]
        public async Task<IActionResult> GetByIdWithUserRoleDetails(Guid Id)
        {
            var obj = await _UserService.GetByIdWithUserRoleDetails(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetHrUserByCnic")]
        public async Task<IActionResult> GetHrUserByCnic(string cnic)
        {
            cnic = Regex.Replace(cnic, @"[^0-9]", "");
            var obj = await _UserService.GetHrUserByCnic(cnic);


            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetHrDoctorListByHealthFacility")]
        public async Task<IActionResult> GetHrDoctorListByHealthFacility()
        {
            
           await _UserService.GetHrDoctorListByHealthFacilityCode();

            return Ok(new ResponseSuccess() );
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
