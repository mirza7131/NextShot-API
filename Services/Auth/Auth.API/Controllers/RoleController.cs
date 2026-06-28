using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.ProfileTypeDto;
using AuthDAL.Models.Dto.RoleDto;
using Azure;
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
    public class RoleController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly RoleService<Role> _RoleService;

        #endregion

        #region Constructor

        public RoleController(ILogger<AuthenticationController> logger, TokenService tokenService, RoleService<Role> RoleService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _RoleService = RoleService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditRoleDto input)
        {
            var obj = await _RoleService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _RoleService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _RoleService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterRoleDto filterRoleDto)
        {
            var response = await _RoleService.GetAllWithPagination(filterRoleDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _RoleService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetByIdWithRoleMenuDetails")]
        public async Task<IActionResult> GetByIdWithRoleMenuDetails(Guid Id)
        {
            var obj = await _RoleService.GetByIdWithRoleMenuDetails(Id);
            return Ok(new ResponseSuccess { data = obj });
        }


        #endregion

        #region Helper Methods


        #endregion
    }
}
