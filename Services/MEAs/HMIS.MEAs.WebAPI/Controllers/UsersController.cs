using HMIS.MEAs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonDTOs.ResponseDTO;
using HMIS.MEAs.Domain.Models.DTO.UsersModel;
namespace HMIS.MEAs.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UsersService _auth;
     //   private readonly TokenService _tokenService;
        #region Constructor

        public UsersController(UsersService auth)
        {

          //  _tokenService = tokenService;
            _auth = auth;

        }


        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(RegisterDTO register)
        {
            var data = await _auth.CreateOrEdit(register);
            return Ok(new ResponseSuccess { data = data });
        }
       
        [HttpPut]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var data = await _auth.DeleteUser(userId);
            return Ok(new ResponseSuccess { data = data });
        }


        #endregion

        #region READ Operations

        [HttpGet]
        [Route("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            var data = await _auth.GetUserList();
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("GetUserTypesList")]
        public async Task<IActionResult> GetUserTypeList()
        {
            var data = await _auth.GetUserTypeList();
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("GetRegionList")]
        public async Task<IActionResult> GetRegionList()
        {
            var data = await _auth.GetRegionList();
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetDivisionList")]
        public async Task<IActionResult> GetDivisionList()
        {
            var data = await _auth.GetDivisionList();
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("GetDistrictList")]
        public async Task<IActionResult> GetDistrictList(string val)
        {
            var data = await _auth.GetDistrictList(val);
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("GetTehsilList")]
        public async Task<IActionResult> GetTehsilList(string val)
        {
            var data = await _auth.GetTehsilList(val);
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("GetZoneList")]
        public async Task<IActionResult> GetZoneList(string val)
        {
            var data = await _auth.GetZoneList(val);
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("GetRoleList")]
        public async Task<IActionResult> GetRoleList()
        {
            var data = await _auth.GetRoleList();
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var data = await _auth.GetUserById(userId);
            return Ok(new ResponseSuccess { data = data });
        }
        #endregion


    }
}
