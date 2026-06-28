using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.UserRole;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRoleController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly UserRoleService<UserRole> _UserRoleService;

        #endregion

        #region Constructor

        public UserRoleController(ILogger<AuthenticationController> logger, TokenService tokenService, UserRoleService<UserRole> UserRoleService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _UserRoleService = UserRoleService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditUserRoleDto input)
        {
            var obj = await _UserRoleService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _UserRoleService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _UserRoleService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _UserRoleService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
