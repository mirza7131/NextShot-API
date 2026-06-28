using CommonDTOs.ResponseDTO;
using HMIS.MEAs.Domain.Models.DTO.UMS;
using HMIS.MEAs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.MEAs.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UMSController : Controller
    {
        private readonly UMSService _ums;
        #region Constructor

        public UMSController(UMSService ums)
        {

            //  _tokenService = tokenService;
            _ums = ums;

        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("UpdateUsersZoneId")]
        public async Task<IActionResult> UpdateUsersZoneId(List<UserZoneChangeDTO> users)
        {
            var data = await _ums.UpdateUsersZoneId(users);
            return Ok(new ResponseSuccess { data = data });
        }

        #endregion
    }
}
