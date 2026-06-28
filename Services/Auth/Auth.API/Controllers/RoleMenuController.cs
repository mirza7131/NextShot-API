using AppCommonMethods;
using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.RoleDto;
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
    public class RoleMenuController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly RoleMenuService<RoleMenu> _RoleMenuService;

        #endregion

        #region Constructor

        public RoleMenuController(ILogger<AuthenticationController> logger, TokenService tokenService, RoleMenuService<RoleMenu> RoleMenuService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _RoleMenuService = RoleMenuService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditRoleMenuDto input)
        {
            var obj = await _RoleMenuService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _RoleMenuService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _RoleMenuService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _RoleMenuService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetRoleMenuAccess")]
        public async Task<IActionResult> GetRoleMenuAccess(Guid? RoleId)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(RoleId))
            {
                var obj = await _RoleMenuService.GetCreateRoleMenuAccess();
                return Ok(new ResponseSuccess { data = obj });
            }
            else
            {
                var obj = await _RoleMenuService.GetEditRoleMenuAccess(RoleId ?? Guid.Empty);
                return Ok(new ResponseSuccess { data = obj });
            }
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
